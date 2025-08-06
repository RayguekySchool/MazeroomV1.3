using UnityEngine;
using System.Collections.Generic;

public class EnemyHealth : MonoBehaviour
{
    [System.Serializable]
    public class BulletResistance
    {
        public string bulletType;
        public int bulletsToKill;
    }

    public List<BulletResistance> resistances;
    private Dictionary<string, int> bulletHits = new Dictionary<string, int>();

    void Start()
    {
        foreach (var res in resistances)
        {
            bulletHits[res.bulletType] = 0;
        }
    }

    public void TakeBullet(string bulletType)
    {
        if (!bulletHits.ContainsKey(bulletType)) return;

        bulletHits[bulletType]++;
        int needed = resistances.Find(x => x.bulletType == bulletType).bulletsToKill;

        if (bulletHits[bulletType] >= needed)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject); // ou coloque animação de morte aqui
    }
}
