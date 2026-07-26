using System;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 6;
    public int currentHealth;

    [SerializeField] TMP_Text deathScreen;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die(); // Impleemnt Game Over or respawn logic here
        }
    }

    void Die()
    {
        Time.timeScale = 0f;
        deathScreen.text = "You Ded";
    }
}
