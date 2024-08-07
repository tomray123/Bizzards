using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementController : MonoBehaviour
{
    private CharacterController controller;
    private FloatingJoystick joystick;
    private HeroManager heroManager;
    private float moveSpeed;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        
        var referenceManager = ReferenceManager.Instance;
        if (referenceManager != null)
        {
            joystick = referenceManager.playerData.joyStick;
            heroManager = referenceManager.playerData.heroManager;
            moveSpeed = heroManager.HeroStatsController.CurrentHeroStats.MoveSpeed;
        }
    }

    private void Update()
    {
        if (joystick == null || heroManager == null) return;

        // Move the hero based on joystick direction
        Vector3 move = new Vector3(joystick.Direction.x, 0, joystick.Direction.y);
        controller.SimpleMove(move * moveSpeed);

        // Rotate the hero towards the movement direction
        if (move != Vector3.zero)
        {
            transform.forward = move;
        }
    }
}
