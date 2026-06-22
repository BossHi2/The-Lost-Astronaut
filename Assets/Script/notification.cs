using TMPro;
using UnityEngine;

public class notification : MonoBehaviour
{
    public float notifTimer = 5f;



    float currTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        currTimer += Time.deltaTime;

        if(currTimer >= notifTimer)
        {
            Destroy(gameObject);
        }
    }

    public void changeText(string name)
    {
        TextMeshProUGUI contents = transform.Find("Image/Text (TMP)").gameObject.GetComponent<TextMeshProUGUI>();

        contents.text = name;
    }

    public void moveUp()
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector2(GetComponent<RectTransform>().anchoredPosition.x, GetComponent<RectTransform>().anchoredPosition.y + 30f);

        if(GetComponent<RectTransform>().anchoredPosition.y + 30f > 100f)
        {
            Destroy(gameObject);

        }
    }


}
