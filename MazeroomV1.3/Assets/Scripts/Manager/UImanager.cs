using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public static UImanager instance;
    [SerializeField]
    TextMeshProUGUI killCounter_TMP;
    public int killCounter;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateKillCounter(int kills)
    {
        killCounter_TMP.text = kills.ToString("0");
    }
}
