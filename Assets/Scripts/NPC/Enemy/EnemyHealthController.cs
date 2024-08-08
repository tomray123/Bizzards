using UnityEngine;

/// <summary>
/// Manages enemy's health, damage, and death.
/// </summary>
public class EnemyHealthController
{
    private float health;

    public event System.Action OnDeath;

    /// <summary>
    /// Sets initial health for the enemy.
    /// </summary>
    /// <param name="initialHealth">Initial health value</param>
    public void SetInitialHealth(float initialHealth)
    {
        health = initialHealth;
    }

    /// <summary>
    /// Applies damage to the enemy.
    /// </summary>
    /// <param name="damage">Amount of damage</param>
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Logic for the enemy's death.
    /// </summary>
    private void Die()
    {
        Debug.Log("Enemy has died.");
        OnDeath?.Invoke();
        // Additional logic for enemy's death
    }

    /// <summary>
    /// Returns the current health of the enemy.
    /// </summary>
    /// <returns>Current health value</returns>
    public float GetHealth() => health;
}
