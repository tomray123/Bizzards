using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

[System.Serializable]
public class AttackData
{
    public AttackStats attackStats;
    public Collider hitboxCollider;
    public string attackTrigger; // Название триггера атаки
}

/// <summary>
/// Manages enemy initialization and handles integration with ReferenceManager.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
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
    public EnemyAnimationController EnemyAnimationController { get; private set; } // Новый контроллер анимаций
    private NavMeshAgent navMeshAgent;
    private Animator enemyAnimator;

    private void Awake()
    {
        // Initialize NavMeshAgent
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Initialize Animator
        enemyAnimator = GetComponent<Animator>();

        // Initialize controllers
        EnemyStatsController = new EnemyStatsController(navMeshAgent);
        EnemyHealthController = new EnemyHealthController();
        EnemyAttackController = new EnemyAttackController();
        EnemyAnimationController = new EnemyAnimationController(enemyAnimator); // Инициализация контроллера анимаций

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

                // Add the attack trigger to the animation controller
                EnemyAnimationController.SetTrigger(attackData.attackTrigger);
            }
            else
            {
                Debug.LogError("Can't Find any attackData or hitbox for attack on enemy " + gameObject.name);
            }
        }
    }

    public void ExecuteAttack(string attackTrigger)
    {
        // Trigger the attack animation
        EnemyAnimationController.SetTrigger(attackTrigger);
        if (enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) {
            float attackAnimationLength = enemyAnimator.GetCurrentAnimatorStateInfo(0).length;
            EnemyAttackController.ExecuteAttack(attackAnimationLength);
        }
    }
}
