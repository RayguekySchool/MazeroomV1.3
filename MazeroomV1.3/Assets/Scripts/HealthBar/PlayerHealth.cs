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

    private void DisableMouseLookAndUnlockCursor()
    {
        // Unlock and show the cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Disable the MouseLook script if present
        MouseLook mouseLook = FindObjectOfType<MouseLook>();
        if (mouseLook != null)
        {
            mouseLook.enabled = false;
        }
    }

    // Método chamado quando o jogador morre
    private void Die()
    {
        Debug.Log("Player morreu!");
        DeleteSliderFillArea();
        // Aqui você pode adicionar animação, respawn ou Game Over
        // Exemplo: Destroy(gameObject);
        // Ativa a tela de derrota e congela o jogo
        LoseScreen loseScreen = FindObjectOfType<LoseScreen>();
        if (loseScreen != null)
        {
            loseScreen.ShowLoseScreen();
        }

        DisableMouseLookAndUnlockCursor();
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
