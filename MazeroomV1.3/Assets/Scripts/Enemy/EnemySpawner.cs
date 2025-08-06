using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public int xPos;
    public int zPos;
    public int EnemyCount;
    public int KillCount;
    public GameObject screen;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (EnemyCount < 10)
        {
            xPos = Random.Range(-30, -20);
            zPos = Random.Range(-60, -20);
            Instantiate(enemy, new Vector3(xPos, -4, zPos), Quaternion.identity);
            yield return new WaitForSeconds(0);
            EnemyCount += 1;
        }
    }

    private void EnemyKilled()
    {
        KillCount += 1;
        EnemyCount -= 1;
        Debug.Log("Enemy Killed: " + KillCount);
    }

    private void Update()
    {
        Debug.Log("Kill Count: " + KillCount);
        if (KillCount < 10 && screen != null)
        {
            screen.SetActive(true);
        }
    }
}