using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickFishing : MonoBehaviour, IPointerClickHandler
{
    MainCharacter mainCharacter;
    [SerializeField] private AudioClip clickDot;
    FishUI fishUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCharacter = GameObject.Find("Main Character").GetComponent<MainCharacter>();
        fishUI = GameObject.Find("FishUI").GetComponent<FishUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(fishUI.currTime != 0)
        {
            mainCharacter.audioSource.PlayOneShot(clickDot, 0.5f);
            Destroy(gameObject);
        }
            
    }

}
