using UnityEngine;
using System.Collections.Generic;

public class EnemyHealthBIG : MonoBehaviour
{
    [System.Serializable]
    public class BulletResistance
    {
        public string bulletType;
        public int bulletsToKill;
        public int kills;
    }

    public ParticleSystem deathParticles;
    public List<BulletResistance> resistances;
    private Dictionary<string, int> bulletHits = new Dictionary<string, int>();

    public static int kills = 0;

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

    public void Die()
    {
        UImanager.instance.killCounter++;
        UImanager.instance.UpdateKillCounter(kills);
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
        kills += 1;
    }
}