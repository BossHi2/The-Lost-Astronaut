using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class brokenSpaceship : MonoBehaviour
{
    [SerializeField] private GameObject centerOfBlackHole;
    [SerializeField] private TextMeshProUGUI wood;
    [SerializeField] private TextMeshProUGUI metal;
    [SerializeField] private TextMeshProUGUI brain;

    [SerializeField] private Button repairBtn;
    MainCharacter mainCharacter;
    Rigidbody2D rb;
    public float blackHoleForce;

    [SerializeField] private GameObject repairSpaceshipUI;

    cursorController cursorCont;


    [SerializeField] private GameObject poof;
    [SerializeField] private Image blackImage;
    [SerializeField] private GameObject fadeCanvas;
    [SerializeField] private AudioSource transitionToEnd;


    [SerializeField] private GameObject arrow;

    public bool isGoingToEndScene;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isGoingToEndScene = false;
        cursorCont = GameObject.Find("CursorController").GetComponent<cursorController>();
        rb = GetComponent<Rigidbody2D>();
        mainCharacter = GameObject.Find("Main Character").GetComponent<MainCharacter>();
    }

    // Update is called once per frame
    void Update()
    {

        
        if (Input.GetKeyDown(KeyCode.Escape) && GameObject.Find("RepairSpaceship"))
        {
            repairSpaceshipUI.SetActive(false);
        }

        if (GameObject.Find("RepairSpaceship"))
        {
            if (checkIfCanRepair())
            {
                repairBtn.interactable = true;
            }
            else
            {
                repairBtn.interactable = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (!poof.GetComponent<poof>().willDestroyBrokenShip)
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

    void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && !mainCharacter.isFishing)
        {
            if (!poof.GetComponent<poof>().willDestroyBrokenShip)
            {
                checkIfCanRepair();
                if(arrow)
                    Destroy(arrow);
                repairSpaceshipUI.SetActive(true);
            }
            else
            {
                isGoingToEndScene = true;
                StartCoroutine(Fade());
            }
            
        }
       
    }

    IEnumerator Fade()
    {
        transitionToEnd.Play();
        fadeCanvas.SetActive(true);
        float currTime = 0f;
        while(currTime < 1.7f)
        {
            currTime += Time.deltaTime;
            blackImage.color = new Color(0, 0, 0, Mathf.Lerp(0f, 1f, currTime/3f));
            yield return null;
        }

        SceneManager.LoadScene("EndScene");
    }

    void OnMouseOver()
    {
        if(!EventSystem.current.IsPointerOverGameObject() && !mainCharacter.isFishing)
            cursorCont.SetToIdentifyCursor();
    }

    void OnMouseExit()
    {
        if(!EventSystem.current.IsPointerOverGameObject())
            cursorCont.SetToDefaultCursor();
    }

    bool checkIfCanRepair()
    {
        wood.text = mainCharacter.woodCount + " / 10";
        metal.text = mainCharacter.metalCount + " / 10";
        brain.text = mainCharacter.monsterBrainCount + " / 30";

        bool canRepair = true;
        if(mainCharacter.woodCount < 10)
        {
            wood.color = Color.red;
            canRepair = false;
        }
        else
        {
            wood.color = Color.green;
        }

        if(mainCharacter.metalCount < 10)
        {
            metal.color = Color.red;
            canRepair = false;
        }
        else
        {
            metal.color = Color.green;
        }

        if(mainCharacter.monsterBrainCount < 30)
        {
            brain.color = Color.red;
            canRepair = false;
        }
        else
        {
            brain.color = Color.green;
        }


        return canRepair;
    }
}
