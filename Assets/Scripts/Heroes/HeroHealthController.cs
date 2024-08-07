using UnityEngine;

/// <summary>
/// Manages hero's health, damage, and death.
/// </summary>
public class HeroHealthController
{
    private float health;

    public event System.Action OnDeath;

    /// <summary>
    /// Sets initial health for the hero.
    /// </summary>
    /// <param name="initialHealth">Initial health value</param>
    public void SetInitialHealth(float initialHealth)
    {
        health = initialHealth;
    }

    /// <summary>
    /// Applies damage to the hero.
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
    /// Logic for the hero's death.
    /// </summary>
    private void Die()
    {
        Debug.Log("Hero has died.");
        OnDeath?.Invoke();
        // Additional logic for hero's death
    }

    /// <summary>
    /// Returns the current health of the hero.
    /// </summary>
    /// <returns>Current health value</returns>
    public float GetHealth() => health;
}
