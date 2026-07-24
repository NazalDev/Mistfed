using UnityEngine;

public class HandgunFire : MonoBehaviour
{
    [SerializeField] AudioSource gunFire;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            gunFire.Play();
        }
    }

}
