using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class MainUI : MonoBehaviour
{
    public MainCharacter mainCharacter;
    [SerializeField] private UnityEngine.UI.Image[] slots;
    public string[] itemAtSlot; //"rod" = fishing rod; "sword" = metal sword

    private List<notification> notifs;

    [SerializeField] private GameObject notificationPrefab;

    [SerializeField] private TextMeshProUGUI woodText;
    [SerializeField] private TextMeshProUGUI metalText;
    [SerializeField] private TextMeshProUGUI monsterBrainText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private GameObject globalVolume;


    Volume volume;
    Vignette vignette;

    public AudioClip inventorySound;
    [SerializeField] private AudioClip pickUpSound;
    [SerializeField] private AudioClip playerDamage;
    
    [SerializeField] private Color originalHotbarColor;
    [SerializeField] private Color highlightHotbarColor;

    [SerializeField] private Image[] icons;
    [SerializeField] private Sprite[] iconSprites; //[0] = fishing rod, [1] = sword

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        volume = globalVolume.GetComponent<Volume>();
        if(!volume.profile.TryGet(out vignette))
        {
            Debug.Log("vignette not found");
        }

        notifs = new List<notification>();
        mainCharacter.currEquipped = 0;
        highlightSlot(0);
        itemAtSlot = new string[2];
    }

    // Update is called once per frame
    void Update()
    {
        if(mainCharacter.isFishing == false)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
            {
                mainCharacter.audioSource.PlayOneShot(inventorySound, 0.5f);
                mainCharacter.currEquipped = 0;
                highlightSlot(0);
            }

            if(Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
            {
                mainCharacter.audioSource.PlayOneShot(inventorySound, 0.5f);
                mainCharacter.currEquipped = 1;
                highlightSlot(1);
            }
        }
        
    }

    void highlightSlot(int index)
    {
        for(int i = 0; i<slots.Length; i++)
        {
            if(i == index)
            {
                slots[i].color = highlightHotbarColor;
            }
            else
            {
                slots[i].color = originalHotbarColor;
            }
        }
    }

    public void addItem(string id)
    {
        int spriteIndex = -1;
        if(id == "rod")
        {
            newNotif("Fishing Rod");
            spriteIndex = 0;
        }
            
        
        if(id == "sword")
        {
            newNotif("Sword");
            spriteIndex = 1;
        }
            

        for(int i = 0; i<itemAtSlot.Length; i++)
        {
            if(string.IsNullOrEmpty(itemAtSlot[i]))
            {
                itemAtSlot[i] = id;
                icons[i].sprite = iconSprites[spriteIndex];
                icons[i].rectTransform.sizeDelta = new Vector2(42f, 100f);
                return;
            }
        }
    }

    public void newNotif(string name)
    {
        GameObject newNotif = Instantiate(notificationPrefab, transform);
        notifs.Add(newNotif.GetComponent<notification>());
        newNotif.GetComponent<notification>().changeText(name);

        mainCharacter.audioSource.PlayOneShot(pickUpSound, 0.5f);

        for(int i = 0; i<notifs.Count; i++)
        {
            if(notifs[i])
                notifs[i].moveUp();
            else
            {
                notifs.Remove(notifs[i]);
                i--;
            }
        }
    }

    public void changeWood(int x)
    {
        mainCharacter.woodCount += x;
        woodText.text = "" + mainCharacter.woodCount;
        
    }
    public void changeMetal(int x)
    {
        mainCharacter.metalCount += x;
        metalText.text = "" + mainCharacter.metalCount;
    }
    public void changeMonsterBrain(int x)
    {
        mainCharacter.monsterBrainCount += x;
        monsterBrainText.text = "" + mainCharacter.monsterBrainCount;
    }

    public void changeHealth(int x)
    {
        mainCharacter.health += x;

        if(mainCharacter.health > 15)
        {
            vignette.intensity.value = 0f;
        } else if(mainCharacter.health > 10)
        {
            vignette.intensity.value = 0.3f;
        }
        else
        {
            vignette.intensity.value = 0.6f;
        }

        if(mainCharacter.health < 10)
        {
            healthText.text = " " + mainCharacter.health + "/20";
        }
        else
        {
            healthText.text = mainCharacter.health + "/20";
        }

        if(x < 0)
        {
            mainCharacter.audioSource.PlayOneShot(playerDamage, 0.5f);
            StartCoroutine(BlinkRedText());
        }
        else
        {
            StartCoroutine(BlinkGreenText());
        }
    }

    IEnumerator BlinkRedText()
    {
        healthText.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        healthText.color = Color.white;
        yield return new WaitForSeconds(0.1f);

        healthText.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        healthText.color = Color.white;
        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator BlinkGreenText()
    {
        healthText.color = Color.green;
        yield return new WaitForSeconds(0.1f);

        healthText.color = Color.white;
        yield return new WaitForSeconds(0.1f);

        healthText.color = Color.green;
        yield return new WaitForSeconds(0.1f);

        healthText.color = Color.white;
        yield return new WaitForSeconds(0.1f);
    }
}
