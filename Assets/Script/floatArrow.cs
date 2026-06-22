using UnityEngine;

public class floatArrow : MonoBehaviour
{
    public float floatSpeed = 4f;
    public float floatHeight = 0.2f;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        // Smoothly moves the arrow up and down over time
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.localPosition = new Vector3(startPos.x, newY, startPos.z);
    }
}
