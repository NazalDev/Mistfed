using System.Collections;
using TMPro;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Current Weapon")]
    public GunData currentGun;

    [Header("Raycast Setup")]
    public Transform raycastOrigin;
    public Transform gunPoint;
    public GameObject gunFireEffect;
    public GameObject hitEffect;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;

    [Header("UI")]
    [SerializeField] private TMP_Text ammoText;

    private int currentAmmo;
    private bool canShoot = true;

    // Still available if any other script wants to read it directly
    public int CurrentAmmo => currentAmmo;
    public int MaxAmmo => currentGun.maxAmmo;

    void Start()
    {
        EquipGun(currentGun);
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && canShoot)
        {
            TryShoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    // Call this to switch weapons, e.g. weaponController.EquipGun(shotgunData)
    public void EquipGun(GunData newGun)
    {
        currentGun = newGun;
        currentAmmo = currentGun.maxAmmo;
        canShoot = true;
        StopAllCoroutines();
        UpdateAmmoUI();
    }

    void TryShoot()
    {
        canShoot = false;

        if (currentAmmo <= 0)
        {
            StartCoroutine(FireEmpty());
            return;
        }

        currentAmmo--;
        FireRaycast();
        UpdateAmmoUI();
        StartCoroutine(FireCooldown());
    }

    void FireRaycast()
    {
        Vector3 fwd = raycastOrigin.forward;

        if (gunFireEffect != null && gunPoint != null)
        {
            GameObject muzzle = Instantiate(gunFireEffect, gunPoint.position, Quaternion.identity);
            Destroy(muzzle, 1f);
        }

        if (audioSource != null && currentGun.fireSound != null)
        {
            audioSource.PlayOneShot(currentGun.fireSound);
        }

        if (Physics.Raycast(raycastOrigin.position, fwd, out RaycastHit hit, currentGun.rayLength))
        {
            if (hitEffect != null)
            {
                GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.identity);
                Destroy(impact, 1f);
            }

            if (hit.collider.CompareTag("Enemy"))
            {
                EnemyAI enemyHealth = hit.collider.GetComponent<EnemyAI>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(currentGun.damage);
                }
            }
        }
    }

    void Reload()
    {
        currentAmmo = currentGun.maxAmmo;
        UpdateAmmoUI();
    }

    // Call this from pickups, e.g. weaponController.AddAmmo(10)
    public void AddAmmo(int amount)
    {
        currentAmmo = Mathf.Min(currentAmmo + amount, currentGun.maxAmmo);
        UpdateAmmoUI();
    }

    void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = currentAmmo + " / " + currentGun.maxAmmo;
        }
    }

    IEnumerator FireCooldown()
    {
        yield return new WaitForSeconds(currentGun.cooldown);
        canShoot = true;
    }

    IEnumerator FireEmpty()
    {
        if (audioSource != null && currentGun.emptySound != null)
        {
            audioSource.PlayOneShot(currentGun.emptySound);
        }
        yield return new WaitForSeconds(currentGun.cooldown);
        canShoot = true;
    }
}