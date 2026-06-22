using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class OxygenCounter : MonoBehaviour
{

    [SerializeField] private Image[] bubbles;
    [SerializeField] private MainCharacter mainCharacter;
    [SerializeField] private AudioClip pop;
    [SerializeField] private GameObject globalVolume;

    float currTime;
    int bubblesIndex;

    DepthOfField depthOfField;
    Volume volume;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        volume = globalVolume.GetComponent<Volume>();
        if(!volume.profile.TryGet(out depthOfField))
        {
            Debug.Log("depth of field not found");
        }
        bubblesIndex = 9;
        currTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        currTime += Time.deltaTime;

        if(currTime >= 120 && bubblesIndex >= 0)
        {
            currTime = 0;
            bubbles[bubblesIndex].GetComponent<Animator>().SetTrigger("pop");
            mainCharacter.audioSource.PlayOneShot(pop, 0.5f);
            bubblesIndex--;
        }

        if(bubblesIndex == 1)
        {
            depthOfField.active = true;
        }

        if(bubblesIndex <= -1)
        {
            mainCharacter.hasStartedDeathSequence = true;
            StartCoroutine(mainCharacter.dead());
        }
    }
}
