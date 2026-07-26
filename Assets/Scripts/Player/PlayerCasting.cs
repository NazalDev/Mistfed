using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCasting : MonoBehaviour
{
    private GameObject raycastedObj;
    public Transform RaycastOrigin;
    public Transform gunPoint;
    public GameObject GunFirePoint;
    public float gunCooldown = 0.5f;
    private bool canShoot = true;
    public GameObject HitPoint;
    [SerializeField] private int rayLength = 10;
    // Update is called once per frame
    void Update()
    {
        Shooting();
    }

    void Shooting()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(RaycastOrigin.position, fwd, out hit, rayLength))
        {
            if (Input.GetMouseButtonDown(0) && canShoot)
            {
                canShoot = false;
                // raycastedObj.GetComponent<EnemyHealth>().TakeDamage(10); // Reduce Enemy health
                GameObject a = Instantiate(GunFirePoint, gunPoint.position, Quaternion.identity);
                GameObject b = Instantiate(HitPoint, hit.point, Quaternion.identity);

                Destroy(a, 1f);
                Destroy(b, 1f);

                if (hit.collider.CompareTag("Enemy"))
                {
                    raycastedObj = hit.collider.gameObject;

                    EnemyHealth enemyHealth = raycastedObj.GetComponent<EnemyHealth>();

                    if (enemyHealth != null)
                    {
                        enemyHealth.damage(2); // change the value into the waepon damage
                    }
                }

                StartCoroutine(shootCooldown());
            }

        }

    }

    IEnumerator shootCooldown()
    {
        yield return new WaitForSeconds(gunCooldown);
        canShoot = true;
    }
}
