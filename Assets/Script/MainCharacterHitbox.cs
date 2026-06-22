using Unity.Cinemachine;
using UnityEngine;

public class MainCharacterHitbox : MonoBehaviour
{
    MainCharacter mainCharacter;

    bool wasHit;
    float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCharacter = transform.parent.gameObject.GetComponent<MainCharacter>();
        timer = 0f;
        wasHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(wasHit == true)
        {
            timer += Time.deltaTime;

            if(timer >= 3f)
            {
                mainCharacter.speed = 10f;
                mainCharacter.gameObject.GetComponent<SpriteRenderer>().color = Color.white;

                wasHit = false;
                timer = 0;
            }
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Enemy>() != null && collision.gameObject.GetComponent<Enemy>().canAttack)
        {
            mainCharacter.gameObject.GetComponent<SpriteRenderer>().color = Color.red;

            wasHit = true;
            mainCharacter.speed = 1f;
            mainCharacter.regenerateTimer = 0f;
            mainCharacter.canRegenerate = false;
            collision.gameObject.GetComponent<Enemy>().canAttack = false;
            mainCharacter.mainUI.changeHealth(-1);

            mainCharacter.gameObject.GetComponent<CinemachineImpulseSource>().GenerateImpulse();
        }
    }
}
