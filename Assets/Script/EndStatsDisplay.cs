using TMPro;
using UnityEngine;


public class EndStatsDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statsText;
    [SerializeField] private GameObject creditCanvas;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioSource audioSource;

    PlayerStats playerStats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();

        int minutes = Mathf.FloorToInt(playerStats.runTime / 60f);
        int seconds = Mathf.FloorToInt(playerStats.runTime % 60f);
        string formattedTime = $"{minutes}m {seconds}s";
        statsText.text = "Time: " + formattedTime + "\nBats Killed: " + playerStats.enemiesKilled + "\nWood Caught: " + playerStats.woodCaught + "\nMetal Caught: " + playerStats.metalCaught;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onCreditClick()
    {
        audioSource.PlayOneShot(clickSound, 0.5f);
        creditCanvas.SetActive(true);
        gameObject.SetActive(false);
    }
}
