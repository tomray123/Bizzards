using UnityEngine;

/// <summary>
/// ScriptableObject class to store enemy stats data.
/// </summary>
[CreateAssetMenu(fileName = "NewEnemyStats", menuName = "EnemyStats")]
public class EnemyStats : ScriptableObject, IEnemyStats
{
    [SerializeField] private string enemyName;      // Enemy's name
    [SerializeField] private float health;          // Enemy's health
    [SerializeField] private float moveSpeed;       // Enemy's movement speed
    [SerializeField] private float attackDamage;    // Enemy's attack damage

    // Properties to access the fields
    public string EnemyName => enemyName;
    public float Health => health;
    public float MoveSpeed => moveSpeed;
    public float AttackDamage => attackDamage;
}
