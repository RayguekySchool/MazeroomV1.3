using UnityEngine;
using System.Collections;

public class SecondGun : MonoBehaviour
{
    public Camera fpsCam;
    public float range = 100f;
    public float damage = 20f;

    public SpriteRenderer muzzleFlash;

    void Start()
    {
        muzzleFlash.enabled = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
            StartCoroutine(ShowMuzzleFlash());
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log("Acertou: " + hit.transform.name);

            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    IEnumerator ShowMuzzleFlash()
    {
        muzzleFlash.enabled = true;
        yield return new WaitForSeconds(0.2f);
        muzzleFlash.enabled = false;
    }
}
