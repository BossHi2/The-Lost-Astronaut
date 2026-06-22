using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class StartGameButtonSound : MonoBehaviour
{
    [SerializeField] private AudioClip buttonSound;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource bgMusicAudioSource;

    [SerializeField] private GameObject fadeCanvas;
    [SerializeField] private Image blackImage;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStartButton()
    {
        audioSource.PlayOneShot(buttonSound);
        StartCoroutine(Fade());
    }
    IEnumerator Fade()
    {
        fadeCanvas.SetActive(true);
        float currTime = 0f;
        while(currTime < 3f)
        {
            currTime += Time.deltaTime;
            bgMusicAudioSource.volume = Mathf.Lerp(1f, 0f, currTime/3f);
            blackImage.color = new Color(0, 0, 0, Mathf.Lerp(0f, 1f, currTime/3f));
            yield return null;
        }

        SceneManager.LoadScene("StartAnimationScene");
    }
}
