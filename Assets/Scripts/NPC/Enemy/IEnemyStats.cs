/// <summary>
/// Interface for enemy stats.
/// </summary>
public interface IEnemyStats
{
    string EnemyName { get; }
    float Health { get; }
    float MoveSpeed { get; }
    float AttackDamage { get; }
}
