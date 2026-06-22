using UnityEngine.UI; 
using UnityEngine;
using System.Collections;
using TMPro;
using Unity.VisualScripting;

public class FishUI : MonoBehaviour
{
    [SerializeField] private MainCharacter mainCharacter;
    [SerializeField] private MainUI mainUI;
    [SerializeField] private Image dotPrefab;
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private Image background;
    [SerializeField] private Sprite woodSprite;
    [SerializeField] private Sprite metalSprite;

    

    bool hasFinished;
    bool isTimerStarted;

    Image[] dots;

    public float currTime;
    [SerializeField] private cursorController cursorCont;

    [SerializeField] private AudioClip fishSound;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currTime = 5f;
        isTimerStarted = false;
        dots = new Image[5];
        hasFinished = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerStarted)
        {
            currTime -= Time.deltaTime;
            currTime = Mathf.Max(currTime, 0f);

            int seconds = (int) currTime;
            int milliseconds = (int) ( (currTime-seconds)*100);

            timer.text = string.Format("{0}.{1:00}", seconds, milliseconds);

            if(hasFinished == false)
            {
                bool finishedClickingTemp = true;
                for(int i = 0; i<5; i++)
                {
                    if(dots[i] != null)
                    {
                        finishedClickingTemp = false;
                        break;
                    }
                }
                hasFinished = finishedClickingTemp;
            }
        }

        

        if (hasFinished)
        {
            StartCoroutine(finishFishing());
        }

        if(currTime == 0f)
        {
            StartCoroutine(finishFailFishing());
        }
    }

    private void OnEnable()
    {
        cursorCont.isUIEnabled = true;
        cursorCont.SetToDefaultCursor();
        mainCharacter.audioSource.PlayOneShot(fishSound, 0.5f);
        StartCoroutine(waitToStart());
    }

    private void OnDisable()
    {
        cursorCont.isUIEnabled = false;
    }

    IEnumerator waitToStart()
    {
        yield return new WaitForSeconds(1f);

        for(int i = 0; i<5; i++)
        {
            Vector2 randPoint = new Vector2(Random.Range(background.GetComponent<RectTransform>().rect.xMin + 30f, background.GetComponent<RectTransform>().rect.xMax - 30f), Random.Range(background.GetComponent<RectTransform>().rect.yMin + 30f, background.GetComponent<RectTransform>().rect.yMax-30f));
            
            dots[i] = Instantiate(dotPrefab, transform);

            dots[i].GetComponent<RectTransform>().anchoredPosition = randPoint;


            int rand = Random.Range(1, 3);
            if(rand == 1)
            {
                dots[i].sprite = woodSprite;
            }
            else
            {
                dots[i].sprite = metalSprite;
            }
        }
        isTimerStarted = true;
    }

    IEnumerator finishFailFishing()
    {
        isTimerStarted = false;
        yield return new WaitForSeconds(1f);

        mainCharacter.isFishing = false;
        mainCharacter.fishUI.SetActive(false);
        mainCharacter.anim.SetBool("isFishing", false);
        hasFinished = false;
        currTime = 5f;
        timer.text = "05.00";
        Destroy(dots[0]);
        Destroy(dots[1]);
        Destroy(dots[2]);
        Destroy(dots[3]);
        Destroy(dots[4]);
    }

    IEnumerator finishFishing()
    {
        isTimerStarted = false;
        yield return new WaitForSeconds(1f);

        int rand = Random.Range(1, 3);

        if(rand == 1)
        {
            mainUI.newNotif("Wood");
            mainUI.changeWood(1);
        }
        if(rand == 2)
        {
            mainUI.newNotif("Metal");
            mainUI.changeMetal(1);
        }

        

        mainCharacter.isFishing = false;
        mainCharacter.fishUI.SetActive(false);
        mainCharacter.anim.SetBool("isFishing", false);
        hasFinished = false;
        currTime = 5f;
        timer.text = "05.00";
        isTimerStarted = false;
        dots[0] = null;
        dots[1] = null;
        dots[2] = null;
        dots[3] = null;
        dots[4] = null;
    }
}
