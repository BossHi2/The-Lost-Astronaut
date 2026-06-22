using UnityEngine;

public class repairSpaceshipUI : MonoBehaviour
{
    [SerializeField] private cursorController cursorCont;
    [SerializeField] private GameObject poof;
    [SerializeField] private MainUI mainUI;
    [SerializeField] private MainCharacter mainCharacter;
    [SerializeField] private AudioClip buildSpaceshipSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        cursorCont.isUIEnabled = true;
        cursorCont.SetToDefaultCursor();
        mainCharacter.audioSource.PlayOneShot(mainUI.inventorySound, 0.5f);
    }

    void OnDisable()
    {
        cursorCont.isUIEnabled = false;
        mainCharacter.audioSource.PlayOneShot(mainUI.inventorySound, 0.5f);
    }


    public void clickRepair()
    {
        mainCharacter.audioSource.PlayOneShot(buildSpaceshipSound, 0.5f);
        gameObject.SetActive(false);
        poof.SetActive(true);
    }
}
