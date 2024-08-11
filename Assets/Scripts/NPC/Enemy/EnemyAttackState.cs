using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the enemy's attack state.
/// </summary>
public class EnemyAttackState : EnemyState
{
    private Transform target;
    private string attackTriggerName; // Название триггера атаки

    /// <summary>
    /// Called when the state is initialized.
    /// </summary>
    public override void OnAwake(EnemyStatesManager statesManager)
    {
        // Initialization logic, if needed
    }

    /// <summary>
    /// Called when the state starts.
    /// </summary>
    public override void OnStart(EnemyStatesManager statesManager)
    {
        // Initialization logic, if needed
    }

    /// <summary>
    /// Called when entering the attack state.
    /// </summary>
    public override void OnStateEnter(EnemyStatesManager statesManager, EnemyStatesData stateData)
    {
        target = stateData.target;

        // Trigger the attack animation
        attackTriggerName = "Attack";
        statesManager.EnemyManager.ExecuteAttack(attackTriggerName);
    }

    /// <summary>
    /// Called when exiting the attack state.
    /// </summary>
    public override void OnStateExit(EnemyStatesManager statesManager)
    {
        // Cleanup or transition logic, if needed
    }

    /// <summary>
    /// Called every frame while the enemy is in the attack state.
    /// </summary>
    public override void OnUpdate(EnemyStatesManager statesManager)
    {
        if (target != null)
        {
            // Calculate the distance to the target
            float distanceToTarget = Vector3.Distance(statesManager.transform.position, target.position);

            // Get the current attack's range from the attack controller
            float attackRange = statesManager.EnemyManager.EnemyAttackController.GetCurrentAttackRange();

            // If the target is out of range, return to chase state
            if (distanceToTarget > attackRange)
            {
                EnemyStatesData chaseStateData = new EnemyStatesData
                {
                    target = target,
                    initialPosition = statesManager.transform.position
                };
                statesManager.SwitchState(statesManager.chaseState, chaseStateData);
            }
            else if (statesManager.EnemyManager.EnemyAttackController.CanAttack())
            {
                statesManager.EnemyManager.EnemyAnimationController.SetBool("isIdle", false);
                // If the attack is ready and within range, trigger it again
                statesManager.EnemyManager.ExecuteAttack(attackTriggerName);
            }
            else
            {
                statesManager.EnemyManager.EnemyAnimationController.SetBool("isIdle", true);
            }
        }
        else
        {
            // If the target is lost, return to patrol state
            statesManager.SwitchState(statesManager.patrolState, new EnemyStatesData());
        }
    }

    /// <summary>
    /// Called when the enemy enters a trigger collider.
    /// </summary>
    public override void OnTriggerEnter(EnemyStatesManager statesManager, Collider other)
    {
        // Handle logic for when the enemy enters a trigger, if needed
    }

    /// <summary>
    /// Called when the enemy stays within a trigger collider.
    /// </summary>
    public override void OnTriggerStay(EnemyStatesManager statesManager, Collider other)
    {
        // Handle logic for when the enemy stays within a trigger, if needed
    }

    /// <summary>
    /// Called when the enemy exits a trigger collider.
    /// </summary>
    public override void OnTriggerExit(EnemyStatesManager statesManager, Collider other)
    {
        // Handle logic for when the enemy exits a trigger, if needed
    }

    /// <summary>
    /// Called when the state is destroyed.
    /// </summary>
    public override void OnDestroy(EnemyStatesManager statesManager)
    {
        // Cleanup logic when the state is destroyed, if needed
    }
}
