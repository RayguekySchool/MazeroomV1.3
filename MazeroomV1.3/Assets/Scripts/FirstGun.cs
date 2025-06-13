using UnityEngine;
using System.Collections;

public class FirstGun : MonoBehaviour
{
    public Camera fpsCam;
    public float range = 100f;
    public float damage = 20f;

    public SpriteRenderer muzzleFlash;

    public int maxAmmon = 10;
    public int currentAmno;
    public float reloadTime = 1f;
    private bool isReloading = false;

    public Animator animator;

    void Start()
    {
        if (currentAmno == -1)
            currentAmno = maxAmmon;
        muzzleFlash.enabled = false;
    }

    private void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    void Update()
    {
        if (isReloading)
            return;

        if (currentAmno <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
            StartCoroutine(ShowMuzzleFlash());
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - .25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(.25f);

        currentAmno = maxAmmon;
        isReloading = false;
    }

    void Shoot()
    {
        currentAmno--;

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
