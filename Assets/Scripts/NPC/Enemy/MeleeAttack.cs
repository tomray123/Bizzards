using UnityEngine;
using System.Collections;

/// <summary>
/// Melee attack implementation for enemies.
/// </summary>
public class MeleeAttack : MonoBehaviour, IAttack
{
    private float damage;
    private float activeTime;
    private float castTime;
    private float cooldownTime;
    private float range;
    private Collider hitbox;
    private bool isAttacking;
    private float lastAttackTime;
    private float attackAnimationLength;

    /// <summary>
    /// Initializes the melee attack with attack stats and corresponding hitbox collider.
    /// </summary>
    /// <param name="attackStats">Attack stats</param>
    /// <param name="hitboxCollider">Hitbox collider</param>
    public void Initialize(AttackStats attackStats, Collider hitboxCollider)
    {
        this.damage = attackStats.damagePerSecond;
        this.activeTime = attackStats.activeTime;
        this.castTime = attackStats.castTime;
        this.cooldownTime = attackStats.cooldownTime;
        this.range = attackStats.range;  // Add range from attack stats
        this.hitbox = hitboxCollider;
        hitbox.enabled = false;
        isAttacking = false;
        lastAttackTime = -cooldownTime;  // Initialize so the attack is ready immediately
    }

    /// <summary>
    /// Sets the damage for the attack.
    /// </summary>
    /// <param name="damage">Damage value</param>
    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    /// <summary>
    /// Gets the range of the attack.
    /// </summary>
    public float GetRange()
    {
        return range;
    }

    /// <summary>
    /// Checks if the attack can be executed based on cooldown.
    /// </summary>
    public bool CanAttack()
    {
        return Time.time >= lastAttackTime + cooldownTime;
    }

    /// <summary>
    /// Executes the melee attack.
    /// </summary>
    public void ExecuteAttack(float attackAnimationLength)

    {
        if (!CanAttack() || isAttacking) return;
        isAttacking = true;
        lastAttackTime = Time.time;  // Record the time when the attack starts
        this.attackAnimationLength = attackAnimationLength;
        StartCoroutine(EnableHitboxRoutine());
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        // Cast time before the attack
        yield return new WaitForSeconds(castTime);

        // Cooldown time after the attack
        yield return new WaitForSeconds(cooldownTime);
        isAttacking = false;
    }

    // Enable hitbox when animation is playing
    private IEnumerator EnableHitboxRoutine()
    {
        // Enable hitbox to detect collision with player
        hitbox.enabled = true;
        yield return new WaitForSeconds(attackAnimationLength);
        hitbox.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Get the player's health controller and apply damage
            var playerHealth = other.GetComponent<HeroManager>().HeroHealthController;
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
