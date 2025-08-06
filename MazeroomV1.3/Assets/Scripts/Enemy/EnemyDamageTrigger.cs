using UnityEngine;

public class EnemyDamageTrigger : MonoBehaviour
{
    public string targetTag = "Player"; // Tag do objeto alvo (ex: Player)

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            // Tenta pegar o componente com a fun��o a ser chamada
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10); // Chama a fun��o com dano de 10 (exemplo)
            }
        }
    }

    // Use isso se estiver usando colisores com isTrigger = true
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10);
            }
        }
    }
}
