using UnityEngine;

public class EnemyHeathTotal : MonoBehaviour
{
    public float enemyHealth;

    public int GetBulletsToKill(float damagePerBullet)
    {
        if (damagePerBullet <= 0) return int.MaxValue;
        return Mathf.CeilToInt(enemyHealth / damagePerBullet);
    }

    public int BodyDamage(float bodyDamagePerBullet)
    {
        return GetBulletsToKill(bodyDamagePerBullet);
    }

    public int LeftArmDamage(float leftArmDamagePerBullet)
    {
        return GetBulletsToKill(leftArmDamagePerBullet);
    }

    public int RightArmDamage(float rightArmDamagePerBullet)
    {
        return GetBulletsToKill(rightArmDamagePerBullet);
    }

    public int LeftLegDamage(float leftLegDamagePerBullet)
    {
        return GetBulletsToKill(leftLegDamagePerBullet);
    }

    public int RightLegDamage(float rightLegDamagePerBullet)
    {
        return GetBulletsToKill(rightLegDamagePerBullet);
    }
}
