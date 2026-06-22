using UnityEngine;
using UnityEngine.EventSystems;

public class Brain : MonoBehaviour
{
    MainUI mainUI;


    public float hoverSpeed = 1f;
    public float height = 0.5f;

    Vector3 startPos;

    cursorController cursorCont;

    bool isDestroyed;

    MainCharacter mainCharacter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isDestroyed = false;
        cursorCont = GameObject.Find("CursorController").GetComponent<cursorController>();
        startPos = transform.position;
        mainUI = GameObject.Find("MainUI").GetComponent<MainUI>();
        mainCharacter = GameObject.Find("Main Character").GetComponent<MainCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * hoverSpeed) * height;

        transform.position = new Vector3(startPos.x, newY,startPos.z);
    }

    void OnMouseDown()
    {
        if(!EventSystem.current.IsPointerOverGameObject() && !mainCharacter.isFishing)
        {
            isDestroyed = true;
            mainUI.newNotif("Brain");
            mainUI.changeMonsterBrain(1);
            cursorCont.SetToDefaultCursor();
            Destroy(gameObject);
        }
        
    }

    void OnMouseOver()
    {
        if(!EventSystem.current.IsPointerOverGameObject() && isDestroyed == false && !mainCharacter.isFishing)
            cursorCont.SetToGrabCursor();
    }

    void OnMouseExit()
    {
        if(!EventSystem.current.IsPointerOverGameObject())
            cursorCont.SetToDefaultCursor();
    }
}
