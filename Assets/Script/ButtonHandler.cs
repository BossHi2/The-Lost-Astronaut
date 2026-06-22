using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHandler : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private AudioClip buttonHover;
    [SerializeField] private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        audioSource.PlayOneShot(buttonHover, 0.5f);
    }

    
    
}
