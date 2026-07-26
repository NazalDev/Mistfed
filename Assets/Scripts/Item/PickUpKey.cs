using UnityEngine;

public class PickUpKey : MonoBehaviour
{
    public GameObject keyOnPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        keyOnPlayer.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            this.gameObject.SetActive(false);
            keyOnPlayer.SetActive(true);
        }
    }
}
