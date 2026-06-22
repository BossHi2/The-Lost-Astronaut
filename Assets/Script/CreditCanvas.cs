using UnityEngine;

public class CreditCanvas : MonoBehaviour
{
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject statsCanvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onBackClick()
    {
        audioSource.PlayOneShot(clickSound, 0.5f);
        statsCanvas.SetActive(true);
        gameObject.SetActive(false);
    }
}
