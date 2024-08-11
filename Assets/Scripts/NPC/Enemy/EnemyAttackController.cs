using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Manages enemy attacks.
/// </summary>
public class EnemyAttackController
{
    private List<IAttack> attacks = new List<IAttack>();
    private IAttack currentAttack;

    /// <summary>
    /// Adds an attack to the list of available attacks.
    /// </summary>
    /// <param name="attack">Attack instance</param>
    public void AddAttack(IAttack attack)
    {
        attacks.Add(attack);
        if (currentAttack == null)
        {
            currentAttack = attack; // Set the first attack as the current attack
        }
    }

    /// <summary>
    /// Executes the current attack.
    /// </summary>
    public void ExecuteAttack(float attackAnimationLength)
    {
        currentAttack?.ExecuteAttack(attackAnimationLength);
    }

    /// <summary>
    /// Switches to the next attack in the list.
    /// </summary>
    public void SwitchToNextAttack()
    {
        if (attacks.Count == 0) return;

        int currentIndex = attacks.IndexOf(currentAttack);
        int nextIndex = (currentIndex + 1) % attacks.Count;
        currentAttack = attacks[nextIndex];
    }

    /// <summary>
    /// Returns the range of the current attack.
    /// </summary>
    public float GetCurrentAttackRange()
    {
        if (currentAttack is MeleeAttack meleeAttack)
        {
            return meleeAttack.GetRange();
        }

        // If other types of attacks are added in the future, handle them here.

        return 0f; // Default to 0 if the attack type is unknown
    }

    /// <summary>
    /// Checks if the current attack can be executed.
    /// </summary>
    public bool CanAttack()
    {
        return currentAttack?.CanAttack() ?? false;
    }
}
