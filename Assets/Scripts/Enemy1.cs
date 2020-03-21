using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy1 : MonoBehaviour
{
    public float startHealth = 160f;
    private float health;
    public List<GameObject> players;
    public ParticleSystem deathSight;
    public ParticleSystem attacking;
    public Image healthBar;
    public float range = 10;
    private new AudioSource audio;
    public AudioClip clipA;
    public AudioClip clipB;

    public enum EnemyStates { Idle, Moving, Attacking }
    public static EnemyStates currentlyStating;
    public GameObject playerTarget;

    private void Start()
    {
        health = startHealth;
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        audio = GetComponent<AudioSource>();
        playerTarget = null;
    }

    void Awake()
    {
        players = new List<GameObject>();
    }

    void Update()
    {
        if (currentlyStating == EnemyStates.Idle)
        {
            StopAllCoroutines();

            if (playerTarget != null)
            {
                GetComponent<NavMeshAgent>().isStopped = false;
                GetComponent<NavMeshAgent>().destination = playerTarget.transform.position;
                if (Vector3.Distance(transform.position, playerTarget.transform.position) <= range)
                {
                    StartCoroutine(Attack());
                }
            }
            else
            {
                Unit[] units = FindObjectsOfType<Unit>();
                playerTarget = units[0].gameObject;
            }
        }

        

        if (health == 0)
        {
            Destroy(gameObject);
            Instantiate(deathSight, gameObject.transform.position, Quaternion.identity);
            audio.PlayOneShot(clipB);
        }
    }

    public void Hurt(float damage)
    {
        health -= damage;
        healthBar.fillAmount = health / startHealth;
    }

    IEnumerator Attack()
    {
        GetComponent<NavMeshAgent>().isStopped = true;
        currentlyStating = EnemyStates.Attacking;
        playerTarget.GetComponent<Unit>().Hurt(20);
        yield return new WaitForSeconds(2);
        Instantiate(attacking, new Vector3(transform.position.x, transform.position.y, transform.position.z + 1), transform.rotation);
        if (playerTarget != null)
        {
            StartCoroutine(Attack());
            audio.PlayOneShot(clipA);
        }
        else
        {
            currentlyStating = EnemyStates.Idle;

        }
    }
}

