using UnityEngine;
using TMPro;

public class PistolaCanvasGun : MonoBehaviour
{
    public Animator animator;
    public float range = 100f;
    public Camera fpsCam;
    public string bulletType = "Pistol";
    public float fireRate = 0.5f;
    public int maxAmmo = 12;
    public int reserveAmmo = 36;
    private int currentAmmo;

    public TextMeshProUGUI ammoText;

    private float nextTimeToFire = 0f;
    private bool isReloading = false;
    private bool hasShotOnce = false;

    void Start()
    {
        currentAmmo = maxAmmo;
        if (ammoText != null)
            ammoText.gameObject.SetActive(false); // Esconde o contador no in�cio
    }

    void Update()
    {
        if (isReloading)
        {
            animator.SetBool("ShootPistol", false); // Impede tiro durante recarga
            return;
        }

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo && reserveAmmo > 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && currentAmmo > 0)
        {
            nextTimeToFire = Time.time + fireRate;
            Shoot();
        }
        else
        {
            // Transi��o autom�tica de volta pro Idle
            animator.SetBool("ShootPistol", false);
            animator.SetBool("ReloadPistol", false);
        }
        if (isReloading) return;
    }


    void Shoot()
    {
        currentAmmo--;
        animator.SetBool("ShootPistol", true); // Ativa tiro
        animator.SetBool("ReloadPistol", false); // Garante que n�o est� recarregando
        animator.SetTrigger("ShootPistol");

        if (!hasShotOnce)
        {
            hasShotOnce = true;
            if (ammoText != null)
                ammoText.gameObject.SetActive(true);
        }

        UpdateAmmoUI();

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeBullet(bulletType);
            }
        }
    }



    System.Collections.IEnumerator Reload()
    {
        isReloading = true;

        animator.SetBool("ShootPistol", false);
        animator.SetBool("ReloadPistol", true);
        animator.ResetTrigger("ShootPistol"); // Garante que n�o entra em conflito
        animator.SetTrigger("ReloadPistol");

        yield return new WaitForSeconds(1.5f); // tempo da anima��o de recarga
        yield return new WaitForSeconds(1.5f); // tempo da recarga

        int neededAmmo = maxAmmo - currentAmmo;
        int ammoToReload = Mathf.Min(neededAmmo, reserveAmmo);

        currentAmmo += ammoToReload;
        reserveAmmo -= ammoToReload;

        UpdateAmmoUI();
        isReloading = false;

        animator.SetBool("ReloadPistol", false);
    }

    void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = $"{currentAmmo} / {reserveAmmo}";
        }
    }
}