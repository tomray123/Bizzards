using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Requires NavMeshAgent component.
[RequireComponent(typeof(NavMeshAgent))]
public class ChasingWithNavigation : ChasingMethod
{
    // Script recalculates Path 1 time per recalculatePathFrequency
    [SerializeField]
    private float recalculatePathFrequency = 0.5f;

    private NavMeshAgent navMeshAgent;
    private float recalculateTimer;
    private bool isChasing = false;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        recalculateTimer = 0f;
        isChasing = false;
    }

    void Update()
    {
        if (isChasing)
        {
            recalculateTimer += Time.deltaTime;

            // Recalculate path
            if (recalculateTimer >= recalculatePathFrequency)
            {
                Move();
                recalculateTimer = 0f;
            }
        }
    }

    // Move agent with navMeshAgent
    private void Move()
    {
        if (target != null)
        {
            navMeshAgent.destination = target.position;
        }
        else
        {
            Debug.LogError("Object '" + this.gameObject.name + "' doesn't have any target!");
        }
    }

    public override void StartChase(Transform target)
    {
        this.target = target;
        isChasing = true;
    }

    public override void StopChase()
    {
        target = null;
        isChasing = false;
        navMeshAgent.destination = transform.position;
    }

    public override float GetDistanceToTarget()
    {
        return navMeshAgent.remainingDistance;
    }

    private void OnDisable()
    {
        StopChase();
    }  
}
