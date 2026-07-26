using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 10;

    public void damage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
