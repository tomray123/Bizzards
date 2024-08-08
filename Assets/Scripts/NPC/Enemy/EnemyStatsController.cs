using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Manages enemy's stats.
/// </summary>
public class EnemyStatsController
{
    private IEnemyStats currentEnemyStats;  // Current enemy's stats
    private NavMeshAgent navMeshAgent;      // NavMeshAgent for movement

    public IEnemyStats CurrentEnemyStats => currentEnemyStats;

    /// <summary>
    /// Constructor to initialize the EnemyStatsController with NavMeshAgent.
    /// </summary>
    /// <param name="navMeshAgent">NavMeshAgent component</param>
    public EnemyStatsController(NavMeshAgent navMeshAgent)
    {
        this.navMeshAgent = navMeshAgent;
    }

    /// <summary>
    /// Initializes the current enemy with given stats and sets the NavMeshAgent speed.
    /// </summary>
    /// <param name="enemyStats">New enemy's stats</param>
    public void InitializeEnemy(IEnemyStats enemyStats)
    {
        currentEnemyStats = enemyStats;
        navMeshAgent.speed = currentEnemyStats.MoveSpeed;
    }
}
