using System;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 6;
    public int currentHealth;

    [SerializeField] TMP_Text deathScreen;

    [Header("Audio")]
    public AudioSource HeartBeat; // only plays when currentHealth <= 2
    public AudioSource HeartBeep; // plays when currentHealth <= 4, speeds up at <= 2

    [Header("Health thresholds")]
    [SerializeField] private int beepThreshold = 4;
    [SerializeField] private int criticalThreshold = 2;

    [Header("Pitch settings for HeartBeep")]
    [SerializeField] private float normalBeepPitch = 1f;
    [SerializeField] private float fastBeepPitch = 1.6f;

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        HeartBeat.loop = true;
        HeartBeep.loop = true;
    }

    void Update()
    {
        if (isDead) return;

        UpdateHeartAudio();
    }

    void UpdateHeartAudio()
    {
        // HeartBeat only plays at critical health
        if (currentHealth <= criticalThreshold)
        {
            if (!HeartBeat.isPlaying)
                HeartBeat.Play();
        }
        else
        {
            if (HeartBeat.isPlaying)
                HeartBeat.Stop();
        }

        // HeartBeep plays at <= beepThreshold, faster at <= criticalThreshold
        if (currentHealth <= criticalThreshold)
        {
            HeartBeep.pitch = fastBeepPitch;
            if (!HeartBeep.isPlaying)
                HeartBeep.Play();
        }
        else if (currentHealth <= beepThreshold)
        {
            HeartBeep.pitch = normalBeepPitch;
            if (!HeartBeep.isPlaying)
                HeartBeep.Play();
        }
        else
        {
            if (HeartBeep.isPlaying)
                HeartBeep.Stop();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die(); // Implement Game Over or respawn logic here
        }
    }

    void Die()
    {
        isDead = true;
        Time.timeScale = 0f;
        deathScreen.text = "You Ded";

        if (HeartBeat.isPlaying) HeartBeat.Stop();
        if (HeartBeep.isPlaying) HeartBeep.Stop();
    }
}