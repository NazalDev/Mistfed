using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Weapons/Gun Data")]
public class GunData : ScriptableObject
{
    [Header("Identity")]
    public string gunName = "Handgun";

    [Header("Stats")]
    public float cooldown = 0.5f;
    public int damage = 2;
    public int maxAmmo = 10;
    public int rayLength = 10;

    [Header("Audio")]
    public AudioClip fireSound;
    public AudioClip emptySound;
}
