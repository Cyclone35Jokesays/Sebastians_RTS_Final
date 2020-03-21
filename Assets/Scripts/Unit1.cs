using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Unit1 : MonoBehaviour
{
    public GameObject cylinder;
    NavMeshAgent agent;
    private float health;
    public float startingHealth = 160f;
    public float stopDistance = 5;
    public ParticleSystem shooting;
    public ParticleSystem deathSight;
    public Image hpBar;
    private new AudioSource audio;
    public AudioClip clipA;
    public AudioClip clipB;
    public enum PlayerStates { Moving, MoveToAttack, Attacking }
    public static PlayerStates currentState;
    private GameObject target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        health = startingHealth;
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (currentState == PlayerStates.MoveToAttack)
        {
            if (target != null)
            {
                if (Vector3.Distance(transform.position, target.transform.position) <= stopDistance)
                {
                    agent.isStopped = true;
                    StartCoroutine(AttackEnemy());
                }
            }
        }

        if (currentState == PlayerStates.Moving)
        {
            agent.isStopped = false;
        }

        if (health <= 0)
        {
            Destroy(this.gameObject);
            Instantiate(deathSight, gameObject.transform.position, Quaternion.identity);
            audio.PlayOneShot(clipB);
        }
    }

    public void SelectChange()
    {
        cylinder.SetActive(true);
    }

    public void DeselectChange()
    {
        cylinder.SetActive(false);
    }

    public void OrderTo(Vector3 tar)
    {
        currentState = PlayerStates.Moving;
        target = null;
        agent.SetDestination(tar);
    }

    public void AttackAction(GameObject target)
    {
        currentState = PlayerStates.MoveToAttack;
        agent.SetDestination(target.transform.position);
        this.target = target;
    }

    IEnumerator AttackEnemy()
    {
        currentState = PlayerStates.Attacking;
        target.GetComponent<Enemy>().Hurt(30);
        yield return new WaitForSeconds(2);
        Instantiate(shooting, new Vector3(transform.position.x, transform.position.y, transform.position.z + 1), transform.rotation);
        if (target != null)
        {
            StartCoroutine(AttackEnemy());
            audio.PlayOneShot(clipA);
        }
        else
        {
            currentState = PlayerStates.Moving;
        }
    }

    public void Hurt(float damage)
    {
        StartCoroutine(Delay2());
        health -= damage;
        hpBar.fillAmount = health / startingHealth;
    }

    IEnumerator Delay1()
    {
        yield return new WaitForSeconds(1);
    }

    IEnumerator Delay2()
    {
        yield return new WaitForSeconds(4);
        health -= 5;
    }
}

