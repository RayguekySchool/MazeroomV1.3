using UnityEngine;
using System.Collections.Generic;

public class EnemyHealth : MonoBehaviour
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

    public void Detected()
    {
        if (transform.tag == "EnemyBody")
        {
            Debug.Log("Body");
        }
        if (transform.tag == "EnemyLarm")
        {
            Debug.Log("LeftArm");
        }
        if (transform.tag == "EnemyRarm")
        {
            Debug.Log("RightArm");
        }
        if (transform.tag == "EnemyLleg")
        {
            Debug.Log("LeftLeg");
        }
        if (transform.tag == "EnemyRleg")
        {
            Debug.Log("RightLeg");
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
