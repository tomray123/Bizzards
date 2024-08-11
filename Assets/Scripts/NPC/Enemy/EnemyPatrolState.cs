using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyState
{
    private DetectionLogic detectionLogic;
    private PatrollingMethod patrollingMethod;
    // Time to wait after swithing from chase to patrolling.
    private float returnToHomeTimeout;
    private SyncedTimer pauseDetectiontimer;

    public override void OnAwake(EnemyStatesManager statesManager)
    {
        pauseDetectiontimer = new SyncedTimer(TimerType.UpdateTick);
        returnToHomeTimeout = statesManager.returnToHomeTimeout;
        detectionLogic = new DetectionLogic(statesManager.fovDegree, statesManager.obstructionMask);
        // Subscribing timer events
        pauseDetectiontimer.TimerFinished += UnPauseDetection;
    }

    public override void OnStart(EnemyStatesManager statesManager)
    {
        patrollingMethod = statesManager.GetComponent<PatrollingMethod>();
    }

    public override void OnStateEnter(EnemyStatesManager statesManager, EnemyStatesData stateData)
    {
        if (stateData.previousState is EnemyChaseState)
        {
            PauseDetection();
            pauseDetectiontimer.Start(returnToHomeTimeout);
        }
        patrollingMethod.enabled = true;
        statesManager.EnemyManager.EnemyAnimationController.SetBool("isPatrol", true);
    }

    public override void OnStateExit(EnemyStatesManager statesManager)
    {
        pauseDetectiontimer.Stop();
        patrollingMethod.Stop();
        patrollingMethod.enabled = false;
        statesManager.EnemyManager.EnemyAnimationController.SetBool("isPatrol", false);
    }

    public override void OnUpdate(EnemyStatesManager statesManager)
    {
        Debug.Log("Patrolling");
    }

    public override void OnTriggerEnter(EnemyStatesManager statesManager, Collider other)
    {
        DetectTarget(statesManager, other);
    }

    public override void OnTriggerStay(EnemyStatesManager statesManager, Collider other)
    {
        DetectTarget(statesManager, other);
    }

    public override void OnTriggerExit(EnemyStatesManager statesManager, Collider other)
    {

    }

    public override void OnDestroy(EnemyStatesManager statesManager)
    {
        // Unsubscribing timer events
        pauseDetectiontimer.TimerFinished -= UnPauseDetection;
    }

    // Logic for detecting the target
    private void DetectTarget(EnemyStatesManager statesManager, Collider other)
    {
        bool canSeeTarget = detectionLogic.IsObjectDetected(statesManager.transform, other.transform);
        if (canSeeTarget)
        {
            EnemyStatesData stateData = new EnemyStatesData();
            stateData.target = other.transform;
            stateData.initialPosition = patrollingMethod.GetPatrollingZoneCenter();
            stateData.previousState = this;
            statesManager.SwitchState(statesManager.chaseState, stateData);
        }
    }

    private void PauseDetection()
    {
        detectionLogic.PauseDetection();
    }

    private void UnPauseDetection()
    {
        detectionLogic.UnPauseDetection();
    }
}
