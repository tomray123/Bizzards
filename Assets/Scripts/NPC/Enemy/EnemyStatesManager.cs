using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatesManager : MonoBehaviour
{
    public float fovDegree = 150f;
    public LayerMask obstructionMask;
    // Distance between enemy and its patrol zone after reaching which enemy will return to patrolling state
    public float returnToHomeMaxDistance = 15f;
    // Time to wait after swithing from chase to patrolling.
    public float returnToHomeTimeout = 2f;

    [HideInInspector] public EnemyState patrolState = new EnemyPatrolState();
    [HideInInspector] public EnemyState chaseState = new EnemyChaseState();
    [HideInInspector] public EnemyState idleState = new EnemyIdleState();
    [HideInInspector] public EnemyState attackState = new EnemyAttackState();
    [HideInInspector] public EnemyState specialState = new EnemySpecialState();
    [HideInInspector] public EnemyState deathState = new EnemyDeathState();

    private EnemyState currentState;

    // Reference to EnemyManager
    public EnemyManager EnemyManager { get; private set; }

    private void Awake()
    {
        // Initialize the EnemyManager reference
        EnemyManager = GetComponent<EnemyManager>();

        currentState = patrolState;
        patrolState.OnAwake(this);
        chaseState.OnAwake(this);
        idleState.OnAwake(this);
        attackState.OnAwake(this);
        specialState.OnAwake(this);
        deathState.OnAwake(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        patrolState.OnStart(this);
        chaseState.OnStart(this);
        idleState.OnStart(this);
        attackState.OnStart(this);
        specialState.OnStart(this);
        deathState.OnStart(this);

        EnemyStatesData stateData = new EnemyStatesData();
        currentState.OnStateEnter(this, stateData);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnUpdate(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(this, other);
    }

    private void OnTriggerStay(Collider other)
    {
        currentState.OnTriggerStay(this, other);
    }

    private void OnTriggerExit(Collider other)
    {
        currentState.OnTriggerExit(this, other);
    }

    private void OnDestroy()
    {
        patrolState.OnDestroy(this);
        chaseState.OnDestroy(this);
        idleState.OnDestroy(this);
        attackState.OnDestroy(this);
        specialState.OnDestroy(this);
        deathState.OnDestroy(this);
    }

    public void SwitchState(EnemyState newState, EnemyStatesData stateData)
    {
        currentState.OnStateExit(this);
        currentState = newState;
        currentState.OnStateEnter(this, stateData);
    }
}
