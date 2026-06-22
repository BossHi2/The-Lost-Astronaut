using UnityEngine;
using Unity.Cinemachine;
using System.Collections;

public class SpaceshipExhaust : MonoBehaviour
{
    [SerializeField] private Animator exhaustAnim;
    bool hasSetTrigger;

    [SerializeField] private GameObject successUI;
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

        while(currTime < 2.333f)
        {
            GetComponent<CinemachineImpulseSource>().GenerateImpulse();
            currTime += 0.2f;
            yield return new WaitForSeconds(0.2f);
        }

        successUI.SetActive(true);
    }
}
