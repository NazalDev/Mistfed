using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private Light flashlightLight;
    [SerializeField] private AudioSource toggleSound;
    [SerializeField] private KeyCode toggleKey = KeyCode.F;
    private bool isOn = false;

    void Start()
    {
        if (flashlightLight != null)
        {
            flashlightLight.enabled = isOn;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleFlashlight();
        }
    }

    void ToggleFlashlight()
    {
        isOn = !isOn;

        if (flashlightLight != null)
        {
            flashlightLight.enabled = isOn;
        }

        if (toggleSound != null)
        {
            toggleSound.Play();
        }
    }
}
