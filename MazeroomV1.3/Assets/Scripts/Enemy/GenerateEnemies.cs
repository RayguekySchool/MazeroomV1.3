using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GenerateEnemies : MonoBehaviour
{
    public GameObject theEnemy;
    public int enemyCount;
    public int maxEnemies;
    public Vector2 spawnXRange;
    public Vector2 spawnZRange;

    private Transform spawnPoint;

    void Start()
    {
        spawnPoint = transform;
        StartCoroutine(EnemyDrop());
    }

    IEnumerator EnemyDrop()
    {
        while (enemyCount < maxEnemies)
        {
            float xPos = Random.Range(spawnXRange.x, spawnXRange.y);
            float zPos = Random.Range(spawnZRange.x, spawnZRange.y);

            Vector3 spawnPosition = spawnPoint.position + new Vector3(xPos, 0, zPos);

            Instantiate(theEnemy, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            enemyCount++;
        }
    }

    public void SetSpawnerPosition(Vector3 newPosition)
    {
        spawnPoint.position = newPosition;
    }
}
