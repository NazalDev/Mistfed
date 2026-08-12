using UnityEngine;

[CreateAssetMenu(fileName = "New Melee Weapon", menuName = "Weapons/Melee Weapon Data")]
public class MeleeWeaponData : ScriptableObject
{
    [Header("Identity")]
    public string weaponName = "Knife";

    [Header("Stats")]
    public float cooldown = 0.6f;
    public int damage = 15;
    public float range = 1.5f;
    public float swingRadius = 0.5f;

    [Header("Audio")]
    public AudioClip[] swingSounds;
}