using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages references to various game components.
/// </summary>
public class ReferenceManager : MonoBehaviour
{
    public static ReferenceManager Instance { get; private set; }

    public PlayerData playerData;
    public List<EnemyManager> enemies = new List<EnemyManager>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        FetchPlayerData();
        FetchEnemies();
    }

    private void FetchPlayerData()
    {
        playerData.joyStick = FindObjectOfType<FloatingJoystick>();
        playerData.playerMovementController = FindObjectOfType<PlayerMovementController>();
        playerData.heroManager = FindObjectOfType<HeroManager>();
    }

    private void FetchEnemies()
    {
        var enemyManagers = FindObjectsOfType<EnemyManager>();
        enemies.AddRange(enemyManagers);
    }
}

[System.Serializable]
public class PlayerData
{
    public FloatingJoystick joyStick;
    public PlayerMovementController playerMovementController;
    public HeroManager heroManager;
}
