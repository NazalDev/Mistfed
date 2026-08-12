using System.Collections;
using TMPro;
using UnityEngine;

public enum WeaponType { Gun, Melee }

public class WeaponController : MonoBehaviour
{
    [Header("Equipped Weapons")]
    public GunData gunData;
    public MeleeWeaponData meleeData;
    public WeaponType currentWeapon = WeaponType.Gun;

    [Header("Switch Keys")]
    public KeyCode gunKey = KeyCode.Alpha1;
    public KeyCode meleeKey = KeyCode.Alpha2;

    [Header("Attack Setup")]
    public Transform raycastOrigin;
    public Transform gunPoint;
    public GameObject fireEffect;
    public GameObject swingEffect;
    public GameObject hitEffect;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;

    [Header("UI")]
    [SerializeField] private TMP_Text ammoText;

    private int currentAmmo;
    private bool canAttack = true;

    public int CurrentAmmo => currentAmmo;
    public int MaxAmmo => gunData.maxAmmo;

    void Start()
    {
        EquipWeapon(currentWeapon);
    }

    void Update()
    {
        HandleSwitching();

        bool firePressed = currentWeapon == WeaponType.Gun
            ? Input.GetMouseButton(0)   // gun: hold to fire
            : Input.GetMouseButtonDown(0); // melee: single swing per press

        if (firePressed && canAttack)
        {
            TryAttack();
        }

        if (currentWeapon == WeaponType.Gun && Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    void HandleSwitching()
    {
        if (Input.GetKeyDown(gunKey) && currentWeapon != WeaponType.Gun)
        {
            EquipWeapon(WeaponType.Gun);
        }
        else if (Input.GetKeyDown(meleeKey) && currentWeapon != WeaponType.Melee)
        {
            EquipWeapon(WeaponType.Melee);
        }
    }

    public void EquipWeapon(WeaponType type)
    {
        currentWeapon = type;
        canAttack = true;
        StopAllCoroutines();

        if (type == WeaponType.Gun)
        {
            currentAmmo = gunData.maxAmmo;
            UpdateAmmoUI();
        }
        else if (ammoText != null)
        {
            ammoText.text = ""; // no ammo display while melee is equipped
        }
    }

    void TryAttack()
    {
        canAttack = false;

        if (currentWeapon == WeaponType.Gun)
        {
            FireGun();
        }
        else
        {
            SwingMelee();
        }
    }

    void FireGun()
    {
        if (currentAmmo <= 0)
        {
            StartCoroutine(FireEmpty());
            return;
        }

        currentAmmo--;
        UpdateAmmoUI();

        Vector3 fwd = raycastOrigin.forward;

        if (fireEffect != null && gunPoint != null)
        {
            GameObject muzzle = Instantiate(fireEffect, gunPoint.position, Quaternion.identity);
            Destroy(muzzle, 0.5f);
        }

        if (audioSource != null && gunData.fireSound != null)
        {
            audioSource.PlayOneShot(gunData.fireSound);
        }

        if (Physics.Raycast(raycastOrigin.position, fwd, out RaycastHit hit, gunData.rayLength))
        {
            SpawnHitEffect(hit.point);
            DamageIfEnemy(hit.collider, gunData.damage);
        }

        StartCoroutine(AttackCooldown(gunData.cooldown));
    }

    void SwingMelee()
    {
        Vector3 fwd = raycastOrigin.forward;

        if (swingEffect != null)
        {
            GameObject swing = Instantiate(swingEffect, raycastOrigin.position, Quaternion.identity);
            Destroy(swing, 0.5f);
        }

        if (audioSource != null && meleeData.swingSound != null)
        {
            audioSource.PlayOneShot(meleeData.swingSound);
        }

        if (Physics.SphereCast(raycastOrigin.position, meleeData.swingRadius, fwd, out RaycastHit hit, meleeData.range))
        {
            SpawnHitEffect(hit.point);

            if (audioSource != null && meleeData.hitSound != null)
            {
                audioSource.PlayOneShot(meleeData.hitSound);
            }

            DamageIfEnemy(hit.collider, meleeData.damage);
        }

        StartCoroutine(AttackCooldown(meleeData.cooldown));
    }

    void SpawnHitEffect(Vector3 point)
    {
        if (hitEffect != null)
        {
            GameObject impact = Instantiate(hitEffect, point, Quaternion.identity);
            Destroy(impact, 0.5f);
        }
    }

    void DamageIfEnemy(Collider col, int damage)
    {
        if (col.CompareTag("Enemy"))
        {
            EnemyAI enemy = col.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    void Reload()
    {
        currentAmmo = gunData.maxAmmo;
        UpdateAmmoUI();
    }

    // Call this from pickups, e.g. weaponController.AddAmmo(10)
    public void AddAmmo(int amount)
    {
        currentAmmo = Mathf.Min(currentAmmo + amount, gunData.maxAmmo);
        UpdateAmmoUI();
    }

    void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = currentAmmo + " / " + gunData.maxAmmo;
        }
    }

    IEnumerator AttackCooldown(float duration)
    {
        yield return new WaitForSeconds(duration);
        canAttack = true;
    }

    IEnumerator FireEmpty()
    {
        if (audioSource != null && gunData.emptySound != null)
        {
            audioSource.PlayOneShot(gunData.emptySound);
        }
        yield return new WaitForSeconds(gunData.cooldown);
        canAttack = true;
    }
}