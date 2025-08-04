using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControler : MonoBehaviour
{
    private PlayerMove player;
    private NavMeshAgent nav;
    public Animator animator; // Added Animator reference

    private Coroutine damageCoroutine;

    [System.Obsolete]
    void Start()
    {
        player = FindObjectOfType<PlayerMove>();
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); // Get Animator component
    }

    void Update()
    {
        if (player != null)
        {
            nav.SetDestination(player.transform.position);

            // Check if enemy is moving
            bool isMoving = nav.velocity.magnitude > 0.1f;
            animator.SetBool("isWalking", isMoving); // Trigger walking animation
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                damageCoroutine = StartCoroutine(DealDamageOverTime(playerHealth));
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

    IEnumerator DealDamageOverTime(PlayerHealth playerHealth)
    {
        while (true)
        {
            playerHealth.TakeDamage(10);
            yield return new WaitForSeconds(2f);
        }
    }
}