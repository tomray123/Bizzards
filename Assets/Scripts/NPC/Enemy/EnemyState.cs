using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    public abstract void OnAwake(EnemyStatesManager statesManager);
    public abstract void OnStart(EnemyStatesManager statesManager);
    public abstract void OnStateEnter(EnemyStatesManager statesManager, EnemyStatesData stateData);
    public abstract void OnStateExit(EnemyStatesManager statesManager);
    public abstract void OnUpdate(EnemyStatesManager statesManager);
    public abstract void OnTriggerEnter(EnemyStatesManager statesManager, Collider other);
    public abstract void OnTriggerStay(EnemyStatesManager statesManager, Collider other);
    public abstract void OnTriggerExit(EnemyStatesManager statesManager, Collider other);
    public abstract void OnDestroy(EnemyStatesManager statesManager);
}
