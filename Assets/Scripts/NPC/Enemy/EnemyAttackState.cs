using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private Transform target;

    public override void OnAwake(EnemyStatesManager statesManager)
    {

    }

    public override void OnStart(EnemyStatesManager statesManager)
    {

    }

    public override void OnStateEnter(EnemyStatesManager statesManager, EnemyStatesData stateData)
    {
        target = stateData.target;

        // Execute the initial attack
        statesManager.EnemyManager.ExecuteAttack();
    }

    public override void OnStateExit(EnemyStatesManager statesManager)
    {

    }

    public override void OnUpdate(EnemyStatesManager statesManager)
    {
        if (target != null)
        {
            // Calculate distance to the target
            float distanceToTarget = Vector3.Distance(statesManager.transform.position, target.position);

            // Get current attack's range from the attack controller
            float attackRange = statesManager.EnemyManager.EnemyAttackController.GetCurrentAttackRange();

            // Return to chase state if the target is out of attack range
            if (distanceToTarget > attackRange)
            {
                EnemyStatesData chaseStateData = new EnemyStatesData();
                chaseStateData.target = target;
                chaseStateData.initialPosition = statesManager.transform.position;
                statesManager.SwitchState(statesManager.chaseState, chaseStateData);
            }
            else if (statesManager.EnemyManager.EnemyAttackController.CanAttack())
            {
                // Continue attacking if within range and the attack is ready
                statesManager.EnemyManager.ExecuteAttack();
            }
        }
        else
        {
            // If the target is lost, return to patrol state
            statesManager.SwitchState(statesManager.patrolState, new EnemyStatesData());
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
