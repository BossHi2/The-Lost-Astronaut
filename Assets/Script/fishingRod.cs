using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class fishingRod : MonoBehaviour
{
    private MainUI mainUI;
    private MainCharacter mainCharacter;

    cursorController cursorCont;

    bool isDestroyed;
    Vector3 startPos;
    public float hoverSpeed = 1f;
    public float height = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        isDestroyed = false;
        cursorCont = GameObject.Find("CursorController").GetComponent<cursorController>();
        mainUI = GameObject.Find("MainUI").GetComponent<MainUI>();
        mainCharacter = GameObject.Find("Main Character").GetComponent<MainCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * hoverSpeed) * height;

        transform.position = new Vector3(startPos.x, newY,startPos.z);
    }

    private void OnMouseDown() {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            isDestroyed = true;
            Destroy(gameObject);
            cursorCont.SetToDefaultCursor();
            mainUI.addItem("rod");
        }
        
    }
    void OnMouseOver()
    {
        if(!EventSystem.current.IsPointerOverGameObject() && isDestroyed == false)
            cursorCont.SetToGrabCursor();
    }

    void OnMouseExit()
    {
        if(!EventSystem.current.IsPointerOverGameObject())
            cursorCont.SetToDefaultCursor();
    }

}
