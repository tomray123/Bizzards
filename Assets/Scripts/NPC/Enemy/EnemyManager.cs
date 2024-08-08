using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Manages enemy initialization and handles integration with ReferenceManager.
/// </summary>
public class EnemyManager : MonoBehaviour
{
    [SerializeField] private EnemyStats initialEnemyStats;

    public EnemyStatsController EnemyStatsController { get; private set; }
    public EnemyHealthController EnemyHealthController { get; private set; }
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        // Initialize NavMeshAgent
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Initialize controllers with NavMeshAgent
        EnemyStatsController = new EnemyStatsController(navMeshAgent);
        EnemyHealthController = new EnemyHealthController();

        // Initialize enemy stats and health
        if (initialEnemyStats != null)
        {
            EnemyStatsController.InitializeEnemy(initialEnemyStats);
            EnemyHealthController.SetInitialHealth(initialEnemyStats.Health);
        }
    }

    private void Start()
    {
        
    }
}
