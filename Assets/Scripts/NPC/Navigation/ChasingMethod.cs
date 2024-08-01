using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChasingMethod : MonoBehaviour
{
    // Target to chase.
    protected Transform target;

    // Method for starting the chase
    public abstract void StartChase(Transform target);

    // Method for stopping the chase
    public abstract void StopChase();

    // Return distance to the target
    public abstract float GetDistanceToTarget();
}
