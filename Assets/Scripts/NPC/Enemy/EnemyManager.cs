using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

[System.Serializable]
public class AttackData
{
    public AttackStats attackStats;
    public Collider hitboxCollider;
}

/// <summary>
/// Manages enemy initialization and handles integration with ReferenceManager.
/// </summary>
public class EnemyManager : MonoBehaviour
{
    [SerializeField] private EnemyStats initialEnemyStats;

    // List to store attack data and corresponding hitbox colliders, visible in the Inspector
    [SerializeField]
    private List<AttackData> attackDataList = new List<AttackData>();

    private Dictionary<AttackStats, Collider> attackDictionary = new Dictionary<AttackStats, Collider>();

    public EnemyStatsController EnemyStatsController { get; private set; }
    public EnemyHealthController EnemyHealthController { get; private set; }
    public EnemyAttackController EnemyAttackController { get; private set; }
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        // Initialize NavMeshAgent
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Initialize controllers with NavMeshAgent
        EnemyStatsController = new EnemyStatsController(navMeshAgent);
        EnemyHealthController = new EnemyHealthController();
        EnemyAttackController = new EnemyAttackController();

        // Initialize enemy stats and health
        if (initialEnemyStats != null)
        {
            EnemyStatsController.InitializeEnemy(initialEnemyStats);
            EnemyHealthController.SetInitialHealth(initialEnemyStats.Health);
            InitializeAttacks();
        }
    }

    private void InitializeAttacks()
    {
        // Populate the dictionary based on the list data
        foreach (var attackData in attackDataList)
        {
            if (attackData.attackStats != null && attackData.hitboxCollider != null)
            {
                attackDictionary.Add(attackData.attackStats, attackData.hitboxCollider);

                // Attach the MeleeAttack component to the same GameObject as the collider
                var meleeAttack = attackData.hitboxCollider.gameObject.AddComponent<MeleeAttack>();
                meleeAttack.Initialize(attackData.attackStats, attackData.hitboxCollider);
                EnemyAttackController.AddAttack(meleeAttack);
            }
        }
    }

    private void Start()
    {

    }

    public void ExecuteAttack()
    {
        EnemyAttackController.ExecuteAttack();
    }
}
