using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private CharacterController controller;
    private ReferenceManager referenceManager;
    private FloatingJoystick joystick;
    [SerializeField]
    private float playerSpeed = 2.0f;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        referenceManager = FindFirstObjectByType<ReferenceManager>();
        if (referenceManager != null)
        {
            joystick = referenceManager.data.joyStick;
        }
    }

    void Update()
    {
        Vector3 move = new Vector3(joystick.Direction.x, 0, joystick.Direction.y);
        controller.SimpleMove(move * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
    }
}