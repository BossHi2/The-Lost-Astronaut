using UnityEngine;

public class poof : MonoBehaviour
{
    public bool willDestroyBrokenShip;

    [SerializeField] private GameObject[] shipParts;
    [SerializeField] private GameObject fixedShip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        willDestroyBrokenShip = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (willDestroyBrokenShip)
        {
            for(int i = 0; i<shipParts.Length; i++)
            {
                shipParts[i].SetActive(false);
            }

            fixedShip.SetActive(true);
        }
    }
}
