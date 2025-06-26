using System.Collections;
<<<<<<< HEAD
=======
using System.Collections.Generic;
>>>>>>> 4835158fd4a09d858bf5e9f664fa258410ebb394
using UnityEngine;
using UnityEngine.AI;

public class EnemyControler : MonoBehaviour
{
<<<<<<< HEAD
    private PlayerMove player;
    private NavMeshAgent nav;

    private Coroutine damageCoroutine;

=======

    PlayerMove player;
    NavMeshAgent nav;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
>>>>>>> 4835158fd4a09d858bf5e9f664fa258410ebb394
    void Start()
    {
        player = FindObjectOfType<PlayerMove>();
        nav = GetComponent<NavMeshAgent>();
    }

<<<<<<< HEAD
    void Update()
    {
        if (player != null)
        {
            nav.SetDestination(player.transform.position);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthBar2 healthBar = collision.gameObject.GetComponent<HealthBar2>();
            if (healthBar != null)
            {
                damageCoroutine = StartCoroutine(DealDamageOverTime(healthBar));
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
        }
    }

    IEnumerator DealDamageOverTime(HealthBar2 healthBar)
    {
        while (true)
        {
            healthBar.ChangeHealth(-10f);
            yield return new WaitForSeconds(2f);
        }
    }
}
=======
    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(player.transform.position);
    }
}
>>>>>>> 4835158fd4a09d858bf5e9f664fa258410ebb394
