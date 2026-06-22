using UnityEngine;
using Unity.Cinemachine;
using System.Collections;

public class SpaceshipExhaust : MonoBehaviour
{
    [SerializeField] private Animator exhaustAnim;
    bool hasSetTrigger;

    [SerializeField] private GameObject successUI;
    [SerializeField] private GameObject statsText;
    [SerializeField] private GameObject creditsBtn;


    [SerializeField] private AudioClip showTextSound;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource bgAudioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hasSetTrigger = false;
        StartCoroutine(shake());
    }

    // Update is called once per frame
    void Update()
    {
        
        if (hasSetTrigger == false && exhaustAnim.GetCurrentAnimatorStateInfo(0).IsName("ExhaustRun"))
        {
            GetComponent<Animator>().SetTrigger("Fly");
            hasSetTrigger = true;
        }
    }

    IEnumerator shake()
    {
        float currTime = 0f;

        while(currTime < 12)
        {
            GetComponent<CinemachineImpulseSource>().GenerateImpulse();
            currTime += 0.2f;
            yield return new WaitForSeconds(0.2f);
        }
        
        yield return new WaitForSeconds(2f);
        audioSource.PlayOneShot(showTextSound, 0.5f);
        successUI.SetActive(true);
        yield return new WaitForSeconds(1f);
        audioSource.PlayOneShot(showTextSound, 0.5f);
        statsText.SetActive(true);
        yield return new WaitForSeconds(1f);
        audioSource.PlayOneShot(showTextSound, 0.5f);
        creditsBtn.SetActive(true);
        StartCoroutine(playBGMusic());
    }
    IEnumerator playBGMusic()
    {
        float currTime = 0f;

        while(currTime < 2f)
        {
            currTime += Time.deltaTime;

            bgAudioSource.volume = Mathf.Lerp(0f, 0.2f, currTime / 2f);
            yield return null;
        }
        
        bgAudioSource.volume = .2f;
    }
}
