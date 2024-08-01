using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceManager : MonoBehaviour
{
    public ReferenceManagerData data;

    // Start is called before the first frame update
    void Awake()
    {
        data = new ReferenceManagerData();
        FetchData();
    }

    private void FetchData()
    {
        data.joyStick = FindFirstObjectByType<FloatingJoystick>();
        data.player = FindFirstObjectByType<PlayerMovementController>();
    }
}

public struct ReferenceManagerData
{
    public FloatingJoystick joyStick;
    public PlayerMovementController player;
}
