using UnityEngine;

public class BlackHoleTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<MainCharacter>() != null)
        {
            collision.gameObject.GetComponent<MainCharacter>().blackHoleForce = 3000f;
        }

        if(collision.gameObject.GetComponent<brokenSpaceship>() != null)
        {
            collision.gameObject.GetComponent<brokenSpaceship>().blackHoleForce = 3000f;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<MainCharacter>() != null)
        {
            collision.gameObject.GetComponent<MainCharacter>().blackHoleForce = 500f;
        }

        if(collision.gameObject.GetComponent<brokenSpaceship>() != null)
        {
            collision.gameObject.GetComponent<brokenSpaceship>().blackHoleForce = 500f;
        }
    }
}
