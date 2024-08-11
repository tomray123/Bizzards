using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PatrollingMethod : MonoBehaviour
{
    // The center of enemy patrolling zone
    protected Vector3 patrollingZoneCenter;
    // Returns center of enemy patrolling zone
    public abstract Vector3 GetPatrollingZoneCenter();
    // Stop coroutine when patroling
    public abstract void Stop();
}
