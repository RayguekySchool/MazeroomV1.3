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
            ammoText.gameObject.SetActive(false); // Esconde o contador no início
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
            // Transição automática de volta pro Idle
            animator.SetBool("ShootPistol", false);
            animator.SetBool("ReloadPistol", false);
        }
        if (isReloading) return;
    }


    void Shoot()
    {
        currentAmmo--;
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
            Debug.Log("Hit: " + hit.transform.name);

            if (hit.transform.tag == "EnemyBody" || hit.transform.tag == "EnemyLarm" || hit.transform.tag == "EnemyRarm" || hit.transform.tag == "EnemyLleg" || hit.transform.tag == "EnemyRleg")
            {
                hit.transform.SendMessage("Detected");
            }
            
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

        animator.ResetTrigger("ShootPistol"); // Garante que não entra em conflito
        animator.SetTrigger("ReloadPistol");

        yield return new WaitForSeconds(1.5f); // tempo da recarga

        int neededAmmo = maxAmmo - currentAmmo;
        int ammoToReload = Mathf.Min(neededAmmo, reserveAmmo);

        currentAmmo += ammoToReload;
        reserveAmmo -= ammoToReload;

        UpdateAmmoUI();
        isReloading = false;
    }

    void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = $"{currentAmmo} / {reserveAmmo}";
        }
    }
}
