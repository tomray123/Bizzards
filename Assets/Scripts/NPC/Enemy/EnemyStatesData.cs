using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EnemyStatesData
{
    // Target to chase
    public Transform target;
    // Initial enemy location point (point to go after chase)
    public Vector3 initialPosition;
    // Previous state
    public EnemyState previousState;
}
