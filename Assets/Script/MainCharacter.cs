using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainCharacter : MonoBehaviour
{

    [SerializeField] private float acceleration;
    public float speed;
    public float blackHoleForce;
    Rigidbody2D rb;

    public bool isFishing;

    public int currEquipped; //0, 1, 2, 3

    [SerializeField] private GameObject centerOfBlackHole;

    public MainUI mainUI;
    public GameObject fishUI;


    float fishTimer;
    float currFishTimer;

    public int woodCount = 0;
    public int metalCount = 0;
    public int monsterBrainCount = 0;

    public int health = 20;



    private Vector2 currentVelocity;

    public bool canRegenerate;
    public float regenerateTimer;
    float timeBetweenHealthRegen;
    float timeBetweenAttack;
    float currTimeBetweenAttack;

    public Animator anim;

    
    
    public AudioSource audioSource;

    [SerializeField] private brokenSpaceship brokenShip;


    [SerializeField] private AudioSource bgMusic;
    [SerializeField] private AudioSource heartbeatMusic;
    [SerializeField] private AudioClip deadSound;
    [SerializeField] private AudioClip swordSwingSound;

    [SerializeField] private cursorController cursorCont;

    bool isHeartBeating;
    public bool hasStartedDeathSequence;


    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currTimeBetweenAttack = 0f;
        timeBetweenAttack = 0.75f;
        hasStartedDeathSequence = false;
        isHeartBeating = false;
        audioSource = GetComponent<AudioSource>();
        timeBetweenHealthRegen = 1f;
        canRegenerate = true;
        rb = GetComponent<Rigidbody2D>();
        isFishing = false;
        fishTimer = 50f;
        anim = GetComponent<Animator>();
        StartCoroutine(fadeInBGMusic());
    }

    IEnumerator fadeInBGMusic()
    {
        float currTime = 0f;
        while(currTime < 3f)
        {
            currTime += Time.deltaTime;
            bgMusic.volume = Mathf.Lerp(0f, 0.2f, currTime/3f);
            yield return null;
        }
    }

    void FixedUpdate()
    {
        if(isFishing == false && !GameObject.Find("RepairSpaceship") && !brokenShip.isGoingToEndScene)
        {
            Vector3 dir = centerOfBlackHole.transform.position - transform.position;

            Vector3 pullDir = dir.normalized;

            if(Vector3.Distance(transform.position, centerOfBlackHole.transform.position) > 1f)
                rb.AddForce(pullDir * blackHoleForce, ForceMode2D.Force);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 maxSpeed = movement.normalized * speed;

        currentVelocity = Vector2.Lerp(currentVelocity, maxSpeed, acceleration * Time.deltaTime);

        if (movement != Vector2.zero && isFishing == false)
        {
            if (Mathf.Abs(movement.y) > 0.1f)
            {
                anim.SetFloat("moveY", movement.y);
                anim.SetFloat("moveX", 0f);
            }
            else
            {
                anim.SetFloat("moveX", movement.x);
                anim.SetFloat("moveY", 0f);
            }
        }


        if (isFishing && currFishTimer <= fishTimer)
        {
            currFishTimer += Time.deltaTime;
        }
        if(currFishTimer >= fishTimer)
        {
            catchFish();
            currFishTimer = 0;
        }
        currTimeBetweenAttack += Time.deltaTime;
  
        if(isFishing == false)
            rb.linearVelocity = currentVelocity;

        if (canRegenerate && !cursorCont.isUIEnabled)
        {
            if(timeBetweenHealthRegen >= 1f) //1 second between health gain
            {
                if(health <= 19)
                {
                    mainUI.changeHealth(1);
                }
                timeBetweenHealthRegen = 0;
            }
            else
            {
                timeBetweenHealthRegen += Time.deltaTime;
            }
        }
        else
        {
            timeBetweenHealthRegen = 0f;
            regenerateTimer += Time.deltaTime;

            if(regenerateTimer >= 5f) //5 seconds without combat to start regenerating
            {
                canRegenerate = true;
            }
        }


        if (Input.GetKeyDown(KeyCode.F))
        {
            if(mainUI.itemAtSlot[currEquipped] == "rod" && isFishing == false)
            {
                isFishing = true;
                anim.SetTrigger("fish");
                anim.SetBool("isFishing", true);
                //cast rod animation
                fishTimer = Random.Range(1f, 5f);
                currFishTimer = 0;
                
            } else if(mainUI.itemAtSlot[currEquipped] == "rod" && isFishing == true && !GameObject.Find("FishUI"))
            {
                isFishing = false;
                anim.SetBool("isFishing", false);
            }else if(mainUI.itemAtSlot[currEquipped] == "sword" && isFishing == false && currTimeBetweenAttack >= timeBetweenAttack)
            {
                currTimeBetweenAttack = 0f;
                anim.SetTrigger("swing");
                audioSource.PlayOneShot(swordSwingSound, 0.5f);
                regenerateTimer = 0f;
                canRegenerate = false;
            }
        }

        if(health <= 0 && !hasStartedDeathSequence)
        {
            hasStartedDeathSequence = true;
            StartCoroutine(dead());
        }
        if(health <= 10 && (isHeartBeating == false))
        {
            isHeartBeating = true;
            StartCoroutine(playHeartBeat());
        }
        else if(health > 10 && isHeartBeating)
        {
            isHeartBeating = false;

            if(heartbeatMusic.volume != 0f)
            {
                StartCoroutine(stopHeartBeat());
            }
                
        }
    }
    public IEnumerator dead()
    {
        anim.SetTrigger("die");
        bgMusic.Stop();
        AudioSource.PlayClipAtPoint(deadSound, Camera.main.transform.position, .5f);
        heartbeatMusic.volume = 0f;
        yield return new WaitForSeconds(4f);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1;
        SceneManager.LoadScene("StartScene");
    }

    IEnumerator playHeartBeat()
    {

        float currTime = 0f;

        while(currTime < 2f)
        {
            currTime += Time.deltaTime;

            bgMusic.volume = Mathf.Lerp(0.2f, 0f, currTime / 2f);
            heartbeatMusic.volume = Mathf.Lerp(0f, 1f, currTime / 2f);
            yield return null;
        }
        
        bgMusic.volume = 0f;
        heartbeatMusic.volume = 1f;
    }
    IEnumerator stopHeartBeat()
    {

        float currTime = 0f;

        while(currTime < 2f)
        {
            currTime += Time.deltaTime;

            bgMusic.volume = Mathf.Lerp(0f, 0.2f, currTime / 2f);
            heartbeatMusic.volume = Mathf.Lerp(1f, 0f, currTime/2f);
            yield return null;
        }
        
        bgMusic.volume = .2f;
        heartbeatMusic.volume = 0f;
    }

    

    void catchFish() //activate the clicking screen
    {
        fishUI.SetActive(true);
    }

    
    

}
