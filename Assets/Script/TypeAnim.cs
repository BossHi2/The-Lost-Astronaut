using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TypeAnim : MonoBehaviour
{
    string text1;
    string text2;
    string text4;
    string text5;
    [SerializeField] private TextMeshProUGUI loreText;
    [SerializeField] private PlayableDirector spaceshipDirector;
    [SerializeField] private PlayableDirector bgDirector;
    [SerializeField] private PlayableDirector blackholeCameraDirector;
    [SerializeField] private GameObject fadeCanvas;
    [SerializeField] private Image blackImage;
    [SerializeField] private AudioSource audioSource;


    Coroutine currCoroutine;
    bool hasStartedFade;

    int currTextNum;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hasStartedFade = false;
        currTextNum = 1;
        text1 = "You are a scientist who has been sent to explore the galaxy.";
        text2 = "But one morning, you got pulled into a giant black hole...";
        text4 = "Even worse, you find out that you only have 20 minutes of oxygen...";
        text5 = "Escape the black hole before it's too late!";
        audioSource.Play();
        currCoroutine = StartCoroutine(typeText(text1));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(currTextNum == 1)
            {
                audioSource.Pause();
                StopCoroutine(currCoroutine);
                currCoroutine = null;
                loreText.text = text1;
                currTextNum++;
            }
            else if(currTextNum == 2)
            {
                if(currCoroutine == null)
                {
                    audioSource.Play();
                    currCoroutine = StartCoroutine(typeText(text2));
                }
                    
                else
                {
                    audioSource.Pause();
                    StopCoroutine(currCoroutine);
                    currCoroutine = null;
                    loreText.text = text2;
                    currTextNum++;
                }
            }
            else if(currTextNum == 3)
            {
                spaceshipDirector.Stop();
                blackholeCameraDirector.Stop();
                bgDirector.Play();
                currTextNum++;
            } else if(currTextNum == 4)
            {
                if(currCoroutine == null)
                {
                    audioSource.Play();
                    currCoroutine = StartCoroutine(typeText(text4));
                }
                else
                {
                    audioSource.Pause();
                    StopCoroutine(currCoroutine);
                    currCoroutine = null;
                    loreText.text = text4;
                    currTextNum++;
                }
            } else if(currTextNum == 5)
            {
                if(currCoroutine == null)
                {
                    audioSource.Play();
                    currCoroutine = StartCoroutine(typeText(text5));
                }
                else
                {
                    hasStartedFade = true;
                    audioSource.Pause();
                    StopCoroutine(currCoroutine);
                    currCoroutine = null;
                    loreText.text = text5;
                    StartCoroutine(Fade());
                    currTextNum++;
                }
            }

            if(currTextNum == 6 && !hasStartedFade)
            {
                hasStartedFade = true;
                audioSource.Pause();
                currCoroutine = null;
                loreText.text = text5;
                StartCoroutine(Fade());
                currTextNum++;
            }
            
        }
    }

    IEnumerator typeText(string txt)
    {
        int currLength = 1;

        while(currLength <= txt.Length)
        {
            loreText.text = txt.Substring(0, currLength);
            currLength++;
            yield return new WaitForSeconds(0.1f);
        }
        currTextNum++;
        audioSource.Pause();
        currCoroutine = null;
    }

    IEnumerator Fade()
    {
        fadeCanvas.SetActive(true);
        float currTime = 0f;
        while(currTime < 3f)
        {
            currTime += Time.deltaTime;
            blackImage.color = new Color(0, 0, 0, Mathf.Lerp(0f, 1f, currTime/3f));
            yield return null;
        }

        SceneManager.LoadScene("MainScene");
    }
}
