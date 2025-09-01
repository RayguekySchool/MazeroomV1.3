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

    [SerializeField] private HealthBarCanva healthBar;
    [SerializeField] private float maxHealth = 100f;
    private float health;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        healthBar = GetComponentInChildren<HealthBarCanva>();
        health = maxHealth;
    }

    void Start()
    {
        foreach (var res in resistances)
        {
            bulletHits[res.bulletType] = 0;
        }
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    public void TakeBullet(string bulletType)
    {
        if (!bulletHits.ContainsKey(bulletType)) return;

        bulletHits[bulletType]++;
        int needed = resistances.Find(x => x.bulletType == bulletType).bulletsToKill;

        // Reduce health based on bullet type (example: each bullet reduces by maxHealth / bulletsToKill)
        float damage = maxHealth / needed;
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);

        healthBar.UpdateHealthBar(health, maxHealth);

        if (bulletHits[bulletType] >= needed || health <= 0)
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