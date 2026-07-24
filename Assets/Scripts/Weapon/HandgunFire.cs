using System.Collections;
using UnityEngine;

public class HandgunFire : MonoBehaviour
{
    [SerializeField] AudioSource gunFire;
    [SerializeField] GameObject gunObject;
    [SerializeField] private float gunCooldown = 0.5f;
    private bool canShoot = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && canShoot)
        {
            canShoot = false;
            StartCoroutine(shootCooldown());
        }
    }

    IEnumerator shootCooldown()
    {
        gunFire.Play();
        yield return new WaitForSeconds(gunCooldown);
        canShoot = true;
    }
}
