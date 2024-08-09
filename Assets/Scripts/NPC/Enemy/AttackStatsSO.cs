using UnityEngine;

public enum AttackType
{
    Melee,
    Ranged,
    AoE
}

/// <summary>
/// ScriptableObject class to store attack data.
/// </summary>
[CreateAssetMenu(fileName = "NewAttack", menuName = "EnemyAttack")]
public class AttackStats : ScriptableObject
{
    public AttackType attackType; // Type of the attack
    public float damagePerSecond; // Damage per second
    public float range; // Range of the attack
    public float activeTime; // Active time of the attack (damage duration)
    public float castTime; // Cast time before the attack starts
    public float cooldownTime; // Cooldown time after the attack
}
