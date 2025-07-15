using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("UI Settings")]
    public Slider healthSlider;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    // Método para receber dano
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            
            Die();
        }
    }

    // Atualiza o slider da UI
    private void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = (float)currentHealth / maxHealth;
        }
    }

    // Método chamado quando o jogador morre
    private void Die()
    {
        Debug.Log("Player morreu!");
        DeleteSliderFillArea();
        // Aqui você pode adicionar animação, respawn ou Game Over
        // Exemplo: Destroy(gameObject);
    }

    private void DeleteSliderFillArea()
    {
        if (healthSlider != null)
        {
            Transform fillArea = healthSlider.transform.Find("Fill Area");
            if (fillArea != null)
            {
                Destroy(fillArea.gameObject);
            }
        }
    }

    // Método para curar o jogador (opcional)
    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
    }
}
