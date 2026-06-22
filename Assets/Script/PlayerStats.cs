using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float runTime;
    public int enemiesKilled;
    public int woodCaught;
    public int metalCaught;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        runTime += Time.deltaTime;
    }
}
