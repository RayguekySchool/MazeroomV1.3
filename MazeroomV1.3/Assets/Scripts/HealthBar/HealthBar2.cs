using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar2 : MonoBehaviour
{
    // Referência para as barras de vida
    public Image healthBar; // Barra de vida principal
    public Image backgroundBar; // Barra traseira
    public TextMeshProUGUI healthText; // Texto que exibe o valor da vida

    // Valor de vida atual e máximo
    public float maxHealth = 100f;
    private float currentHealth;

    // Tempo de atraso para a barra traseira
    public float delayTime = 1f;
    private float targetHealth;

    // Timer de recuperação de vida
    public float recoveryTime = 10f; // Tempo de espera antes de começar a recuperação
    private float recoveryTimer = 0f;

    // Variáveis para o efeito de transparência do painel de dano
    private float damageAlphaSpeed = 1f; // Velocidade do aumento de alpha do painel de dano
    private float maxAlpha = 0.7f; // Alpha máximo do painel

    void Start()
    {
        // Inicializar a vida
        currentHealth = maxHealth;
        targetHealth = currentHealth;
        UpdateHealthBar();
    }

    void Update()
    {
        // Atualiza a barra de fundo com um delay
        if (Mathf.Abs(backgroundBar.fillAmount - healthBar.fillAmount) > 0.01f)
        {
            // Interpola a barra de fundo para acompanhar a barra de vida com atraso
            backgroundBar.fillAmount = Mathf.Lerp(backgroundBar.fillAmount, healthBar.fillAmount, delayTime * Time.deltaTime);
        }

        // Recuperação de vida após 10 segundos
        if (recoveryTimer >= recoveryTime)
        {
            // Recupera 10 de vida por segundo
            ChangeHealth(10f * Time.deltaTime);
        }
        else
        {
            recoveryTimer += Time.deltaTime;
        }
    }

    // Método para aplicar dano ou cura
    public void ChangeHealth(float amount)
    {
        // Alterando a saúde e garantindo que ela não ultrapasse os limites
        float previousHealth = currentHealth;
        currentHealth = Mathf.Clamp(currentHealth + amount, 0f, maxHealth);
        targetHealth = currentHealth;

        // Atualiza a barra de vida imediatamente
        UpdateHealthBar();

        // Reinicia o timer de recuperação quando o jogador toma dano
        if (amount < 0)
        {
            recoveryTimer = 0f;
        }
    }

    // Atualiza as barras e o texto da vida
    private void UpdateHealthBar()
    {
        // Atualiza a barra de vida
        healthBar.fillAmount = currentHealth / maxHealth;

        // Atualiza o texto da vida
        healthText.text = Mathf.Floor(currentHealth).ToString() + "/" + Mathf.Floor(maxHealth).ToString();
    }
}
