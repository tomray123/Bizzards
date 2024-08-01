using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Requires NavMeshAgent component.
[RequireComponent(typeof(NavMeshAgent))]
public class PatrollingSpecificPoints : PatrollingMethod
{
    [SerializeField]
    private List<Transform> patrolPoints;
    [SerializeField]
    private float breakTime = 2f;
    // Script recalculates Path 1 time per recalculatePathFrequency
    [SerializeField]
    private float recalculatePathFrequency = 0.5f;

    private NavMeshAgent navMeshAgent;
    private Vector3 currentTarget;
    private int currentTargetIndex;
    private bool canWalk = true;
    private float recalculateTimer;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        currentTargetIndex = 0;
        recalculateTimer = 0f;
        currentTarget = patrolPoints[currentTargetIndex].position;
        CalculatePatrollingZoneCenter();
    }

    private void Update()
    {
        recalculateTimer += Time.deltaTime;

        if (canWalk)
        {
            // Recalculate path
            if (recalculateTimer >= recalculatePathFrequency)
            {
                Move();
                recalculateTimer = 0f;
            }
            
            // Checking if agent reached a target
            if (!navMeshAgent.pathPending)
            {
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                {
                    if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                    {
                        StartCoroutine(TakeBreak());
                    }
                }
            }
        }
    }

    // Take pause between patrolling
    private IEnumerator TakeBreak()
    {
        canWalk = false;
        yield return new WaitForSeconds(breakTime);
        canWalk = true;
        ChangeNextPoint();
    }

    // Select next destination
    private void ChangeNextPoint()
    {
        currentTargetIndex++;
        if (currentTargetIndex >= patrolPoints.Count)
        {
            currentTargetIndex = 0;
        }
    }

    // Move agent with navMeshAgent
    private void Move()
    {
        currentTarget = patrolPoints[currentTargetIndex].position;
        navMeshAgent.destination = currentTarget;
    }

    // Returns center of enemy patrolling zone
    public override Vector3 GetPatrollingZoneCenter()
    {
        return patrollingZoneCenter;
    }

    // Calculates position of enemy patrolling zone center
    private void CalculatePatrollingZoneCenter()
    {
        Vector3 summ = new Vector3(0, 0, 0);
        foreach(Transform patrolPoint in patrolPoints)
        {
            summ += patrolPoint.position;
        }
        patrollingZoneCenter = summ / patrolPoints.Count;

        //REMOVE DEBUG
        GameObject debugSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        debugSphere.transform.position = patrollingZoneCenter;
    }
}
