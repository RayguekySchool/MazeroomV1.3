using UnityEngine;
using TMPro;

public class UImanager : MonoBehaviour
{
    public static UImanager instance;

    [Header("UI Elements")]
    public TextMeshProUGUI killCounter1Text;
    public TextMeshProUGUI killCounter2Text;

    private void Awake()
    {
        // Garante que s� exista um UImanager ativo
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // opcional � mant�m entre cenas
    }

    public void UpdateKillCounter1(int value)
    {
        if (killCounter1Text != null)
            killCounter1Text.text = $"Kills: {value}";
        else
            Debug.LogWarning("[UImanager] killCounter1Text n�o est� atribu�do!");
    }

    public void UpdateKillCounter2(int value)
    {
        if (killCounter2Text != null)
            killCounter2Text.text = $"Kills2: {value}";
        else
            Debug.LogWarning("[UImanager] killCounter2Text n�o est� atribu�do!");
    }
}
