using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Scripting.APIUpdating;
using Unity.Cinemachine;


public class Enemy : MonoBehaviour
{
    public int health;

    private Animator anim;

    GameObject mainChar;
    public float overshootDist;



    bool isGoingToPlayer;
    bool isOvershooting;

    Vector2 moveDir;

    public bool isFinishedSpawning;

    bool hasActivatedDeathSequence;

    private ParticleSystem bloodSplatter;


    public bool canAttack;
    public float attackCooldown;

    float currCooldownTimer;

    [SerializeField] private AudioClip screech;

    [SerializeField] private GameObject brain;
    [SerializeField] private AudioClip enemyHitSound;
    [SerializeField] private AudioClip enemyDieSound;
    

    private brokenSpaceship brokenShip;
    private PlayerStats playerStats;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
        currCooldownTimer = 0f;
        canAttack = true;
        hasActivatedDeathSequence = false;
        isFinishedSpawning = false;
        isGoingToPlayer = true;
        isOvershooting = false;
        mainChar = GameObject.Find("Main Character");
        brokenShip = GameObject.Find("Broken Spaceship").GetComponent<brokenSpaceship>();
        bloodSplatter = transform.Find("Blood Splatter").GetComponent<ParticleSystem>();
        anim = GetComponent<Animator>();

        GetComponent<NavMeshAgent>().updateRotation = false;
        GetComponent<NavMeshAgent>().updateUpAxis = false;


        GetComponent<NavMeshAgent>().isStopped = true;

        GetComponent<AudioSource>().PlayOneShot(screech, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("FishUI") || GameObject.Find("RepairSpaceship") || brokenShip.isGoingToEndScene)
        {
            GetComponent<NavMeshAgent>().isStopped = true;
        }
        else
        {
            GetComponent<NavMeshAgent>().isStopped = false;
        }

        if(canAttack == false)
        {
            currCooldownTimer += Time.deltaTime;
        }
        if(currCooldownTimer >= attackCooldown)
        {
            canAttack = true;
            currCooldownTimer = 0f;
        }
        if (isFinishedSpawning)
        {
            GetComponent<NavMeshAgent>().isStopped = false;
        }
        if(Mathf.Abs(GetComponent<NavMeshAgent>().velocity.x) > 0.1f && Mathf.Abs(GetComponent<NavMeshAgent>().velocity.y) > 0.1f)
        {
            anim.SetFloat("moveX", 0f);
        }
        else
        {
            anim.SetFloat("moveX", GetComponent<NavMeshAgent>().velocity.x);
        }
        
        anim.SetFloat("moveY", GetComponent<NavMeshAgent>().velocity.y);
        if(health <= 0 && (hasActivatedDeathSequence == false) )
        {
            playerStats.enemiesKilled++;
            hasActivatedDeathSequence = true;
            mainChar.GetComponent<MainCharacter>().audioSource.PlayOneShot(enemyDieSound, 0.5f);
            GetComponent<NavMeshAgent>().isStopped = true;
            Color col = GetComponent<Renderer>().material.color;
            col.a = 0f;
            GetComponent<Renderer>().material.color = col;
            bloodSplatter.Play();
            

            float totalDuration = bloodSplatter.main.duration + bloodSplatter.main.startLifetime.constantMax;

            Destroy(gameObject, totalDuration);
            Instantiate(brain, transform.position, Quaternion.identity);
            
        }

        if (isGoingToPlayer && hasActivatedDeathSequence == false)
        {
            GetComponent<NavMeshAgent>().SetDestination(mainChar.transform.position);
            GetComponent<NavMeshAgent>().autoBraking = false;

            if(((Vector2)mainChar.transform.position - (Vector2)transform.position).normalized != Vector2.zero)
                moveDir = ((Vector2)mainChar.transform.position - (Vector2)transform.position).normalized;

            if(GetComponent<NavMeshAgent>().remainingDistance <= 0.5f)
            {
                isGoingToPlayer = false;
                isOvershooting = true;

                Vector2 overshootTarget = (Vector2) mainChar.transform.position + moveDir*overshootDist;
                GetComponent<NavMeshAgent>().autoBraking = true;

                GetComponent<NavMeshAgent>().SetDestination(overshootTarget);
            }
        }

        if(isOvershooting && GetComponent<NavMeshAgent>().remainingDistance <= GetComponent<NavMeshAgent>().stoppingDistance && hasActivatedDeathSequence == false)
        {
            isOvershooting = false;
            isGoingToPlayer = true;
        }

    }

    public void takeDamage(int dmg)
    {
        health -= dmg;
        if(health > 0)
        {
            mainChar.GetComponent<MainCharacter>().audioSource.PlayOneShot(enemyHitSound, 0.5f);
        }
        Vector2 knockbackDir = (transform.position - mainChar.gameObject.transform.position).normalized;
        GetComponent<NavMeshAgent>().velocity = knockbackDir * 10f;
        mainChar.gameObject.GetComponent<CinemachineImpulseSource>().GenerateImpulse(0.1f);
        StartCoroutine(HitPause());
        StartCoroutine(takeDamageFlash());
    }

    IEnumerator HitPause()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(0.1f);
        Time.timeScale = 1f;
    }

    IEnumerator takeDamageFlash()
    {
        GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

}
