using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GenerateEnemies : MonoBehaviour
{
    public GameObject theEnemy;
    public int xPos;
    public int zPos;
    public int enemyCount;
    public int kills;
    public TextMeshProUGUI killCounter;

    void Start()
    {
        StartCoroutine(EnemyDrop());
    }

    private void Update()
    {
        killCounter.text = kills.ToString("0");
    }

    IEnumerator EnemyDrop()
    {
        while (enemyCount < 10)
        {
            xPos = Random.Range(-40, -15);
            zPos = Random.Range(-8, -35);
            Instantiate(theEnemy, new Vector3(xPos, 0, zPos), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            enemyCount += 1;
        }
    }
}