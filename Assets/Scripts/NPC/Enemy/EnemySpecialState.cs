using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpecialState : EnemyState
{
    public override void OnAwake(EnemyStatesManager statesManager)
    {

    }

    public override void OnStart(EnemyStatesManager statesManager)
    {

    }

    public override void OnStateEnter(EnemyStatesManager statesManager, EnemyStatesData stateData)
    {
        statesManager.EnemyManager.EnemyAnimationController.SetBool("isSpecial", true);
    }

    public override void OnStateExit(EnemyStatesManager statesManager)
    {
        statesManager.EnemyManager.EnemyAnimationController.SetBool("isSpecial", false);
    }

    public override void OnUpdate(EnemyStatesManager statesManager)
    {

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
