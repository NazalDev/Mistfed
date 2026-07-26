using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private GameObject key;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            key = GameObject.FindWithTag("Key");
            if (key != null && key.activeSelf)
            {
                this.gameObject.SetActive(false);
                key.SetActive(false);
            }
        }
    }
}
