using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public static UImanager instance;
    [SerializeField]
    TextMeshProUGUI killCounter_TMP1;
    [SerializeField]
    TextMeshProUGUI killCounter_TMP2;
    public int killCounter1;
    public int killCounter2;

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

    public void UpdateKillCounter1(int kills)
    {
        killCounter1 = kills;
        if (killCounter_TMP1 != null)
            killCounter_TMP1.text = kills.ToString("0");
    }

    public void UpdateKillCounter2(int kills)
    {
        killCounter2 = kills;
        if (killCounter_TMP2 != null)
            killCounter_TMP2.text = kills.ToString("0");
    }
}
