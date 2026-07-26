using UnityEngine;

public class AmmoCollect : MonoBehaviour
{
    [SerializeField] AudioSource collectAmmo;
    [SerializeField] private int ammoAmount = 10;

    private bool playerInRange = false;
    private WeaponController playerWeapon;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerWeapon = other.GetComponent<WeaponController>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            playerWeapon = null;
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (playerWeapon != null)
            {
                playerWeapon.AddAmmo(ammoAmount);
            }

            if (collectAmmo != null)
            {
                collectAmmo.Play();
            }

            GetComponent<SphereCollider>().enabled = false;
            Destroy(gameObject);
        }
    }
}
