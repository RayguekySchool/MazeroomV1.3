using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControler : MonoBehaviour
{
    private PlayerMove player;
    private NavMeshAgent nav;

    private Coroutine damageCoroutine;

    [System.Obsolete]
    void Start()
    {
        player = FindObjectOfType<PlayerMove>();
        nav = GetComponent<NavMeshAgent>();
    }

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