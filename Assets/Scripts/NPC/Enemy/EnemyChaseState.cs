using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyState
{
    private ChasingMethod chasingMethod;
    private Transform target;
    // Distance between enemy and its patrol zone after reaching which enemy will return to patrolling state
    private float returnToHomeMaxDistance;
    private Vector3 homePosition;

    public override void OnAwake(EnemyStatesManager statesManager)
    {
        returnToHomeMaxDistance = statesManager.returnToHomeMaxDistance;
    }

    public override void OnStart(EnemyStatesManager statesManager)
    {
        chasingMethod = statesManager.GetComponent<ChasingMethod>();
    }

    public override void OnStateEnter(EnemyStatesManager statesManager, EnemyStatesData stateData)
    {
        chasingMethod.enabled = true;
        homePosition = stateData.initialPosition;
        target = stateData.target;
        if (target != null)
        {
            chasingMethod.StartChase(target);
        }
        else
        {
            Debug.LogError("Don't have any target!");
        }
    }

    public override void OnStateExit(EnemyStatesManager statesManager)
    {
        chasingMethod.enabled = false;
    }

    public override void OnUpdate(EnemyStatesManager statesManager)
    {
        if (target != null)
        {
            // Calculate distance to the target
            float distanceToTarget = Vector3.Distance(statesManager.transform.position, target.position);

            // Get current attack's range from the attack controller
            float attackRange = statesManager.EnemyManager.EnemyAttackController.GetCurrentAttackRange();

            // Switch to attack state if within attack range
            if (distanceToTarget <= attackRange)
            {
                EnemyStatesData attackStateData = new EnemyStatesData();
                attackStateData.target = target;
                chasingMethod.StopChase();
                statesManager.SwitchState(statesManager.attackState, attackStateData);
                return;
            }
        }

        // Chase logic
        // Return to home zone when too far from it
        float distanceToHome = Vector3.Distance(homePosition, statesManager.transform.position);
        if (distanceToHome > returnToHomeMaxDistance)
        {
            EnemyStatesData stateData = new EnemyStatesData();
            stateData.previousState = this;
            statesManager.SwitchState(statesManager.patrolState, stateData);
        }
    }

    public override void OnTriggerEnter(EnemyStatesManager statesManager, Collider other)
    {

    }

    public override void OnTriggerStay(EnemyStatesManager statesManager, Collider other)
    {

    }

    public override void OnTriggerExit(EnemyStatesManager statesManager, Collider other)
    {

    }

    public override void OnDestroy(EnemyStatesManager statesManager)
    {

    }
}
