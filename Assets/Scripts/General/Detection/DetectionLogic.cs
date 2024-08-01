using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionLogic
{
    // Filed of view angle
    private float fovAngle;
    private LayerMask obstructionMask;
    private bool isPaused;

    public DetectionLogic(float fovAngle, LayerMask obstructionMask, bool isPaused=false)
    {
        this.fovAngle = fovAngle;
        this.obstructionMask = obstructionMask;
        this.isPaused = isPaused;
    }

    public bool IsObjectDetected(Transform seekerObject, Transform searchedObject)
    {
        if (!isPaused)
        {
            Vector3 targetPosition = searchedObject.position;
            Vector3 directionToTarget = (targetPosition - seekerObject.position).normalized;

            // Check if in field of view angle
            if (Vector3.Angle(seekerObject.forward, directionToTarget) < fovAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(seekerObject.position, targetPosition);
                // Check if there is no obstacle between seeker and searched objects
                if (!Physics.Raycast(seekerObject.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public void PauseDetection()
    {
        isPaused = true;
    }

    public void UnPauseDetection()
    {
        isPaused = false;
    }
}
