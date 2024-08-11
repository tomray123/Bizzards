using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Manages enemy animations with smooth transitions.
/// </summary>
public class EnemyAnimationController
{
    private Animator animator;

    /// <summary>
    /// Initializes the animation controller with the given animator.
    /// </summary>
    /// <param name="animator">Animator component</param>
    public EnemyAnimationController(Animator animator)
    {
        this.animator = animator;
    }

    /// <summary>
    /// Triggers a one-time animation using a Trigger parameter.
    /// </summary>
    /// <param name="triggerName">Name of the trigger parameter</param>
    public void SetTrigger(string triggerName)
    {
        animator.SetTrigger(triggerName);
    }

    /// <summary>
    /// Sets a boolean parameter to control continuous animations (e.g., movement).
    /// </summary>
    /// <param name="boolName">Name of the boolean parameter</param>
    /// <param name="value">Boolean value to set</param>
    public void SetBool(string boolName, bool value)
    {
        animator.SetBool(boolName, value);
    }

    /// <summary>
    /// Sets a float parameter to control smooth transitions based on a float value.
    /// </summary>
    /// <param name="floatName">Name of the float parameter</param>
    /// <param name="value">Float value to set</param>
    public void SetFloat(string floatName, float value)
    {
        animator.SetFloat(floatName, value);
    }
}
