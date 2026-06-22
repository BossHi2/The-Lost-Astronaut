using System.Collections;
using NUnit.Framework.Interfaces;
using TMPro;
using UnityEngine;

public class cursorController : MonoBehaviour
{
    [SerializeField] private Texture2D defaultCursor;
    [SerializeField] private Texture2D clickCursor;
    [SerializeField] private Texture2D identifyCursor;
    [SerializeField] private Texture2D grabCursor;
    [SerializeField] private TextMeshProUGUI healthText;


    bool isSpecialCursor;
    bool isBlinking;
    public bool isUIEnabled;

    private Coroutine blinkCoroutine;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isBlinking = false;
        isUIEnabled = false;
        isSpecialCursor = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && (isSpecialCursor == false))
        {
            Vector2 hotspot = new Vector2(clickCursor.width / 2f, clickCursor.height / 2f);
            Cursor.SetCursor(clickCursor, hotspot, CursorMode.Auto);
        } 

        if(Input.GetMouseButtonUp(0) && (isSpecialCursor == false))
        {
            SetToDefaultCursor();
        }

        if (isUIEnabled && !isBlinking)
        {
            isBlinking = true;
            blinkCoroutine = StartCoroutine(blinkHealth());
        } else if(!isUIEnabled && isBlinking)
        {
            isBlinking = false;
            StopCoroutine(blinkCoroutine);
            healthText.color = Color.white;
        }
    }
    IEnumerator blinkHealth()
    {
        while (true)
        {
            healthText.color = Color.clear;
            yield return new WaitForSeconds(0.5f);

            healthText.color = Color.white;
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void SetToGrabCursor()
    {
        if(isUIEnabled == false)
        {
            Vector2 hotspot = new Vector2(grabCursor.width / 2f, grabCursor.height / 2f);
            Cursor.SetCursor(grabCursor, hotspot, CursorMode.Auto);
            isSpecialCursor = true;
        }
        
    }

    public void SetToIdentifyCursor()
    {
        if(isUIEnabled == false)
        {
            Vector2 hotspot = new Vector2(identifyCursor.width / 2f, identifyCursor.height / 2f);
            Cursor.SetCursor(identifyCursor, hotspot, CursorMode.Auto);

            isSpecialCursor = true;
        }
        
    }

    public void SetToDefaultCursor()
    {
        Vector2 hotspot = new Vector2(defaultCursor.width / 2f, defaultCursor.height / 2f);
        Cursor.SetCursor(defaultCursor, hotspot, CursorMode.Auto);
        isSpecialCursor = false;
    }
}
