using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySpawner : MonoBehaviour
{
    [Header("Pontos de Spawn")]
    public List<Transform> spawnPoints = new List<Transform>();

    public List<Transform> GetClosestSpawnPoints(Vector3 playerPos, int count)
    {
        return spawnPoints
            .OrderBy(sp => Vector3.Distance(playerPos, sp.position))
            .Take(count)
            .ToList();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (var sp in spawnPoints)
        {
            if (sp != null)
                Gizmos.DrawSphere(sp.position, 0.5f);
        }
    }
}
