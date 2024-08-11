using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Configuration for a specific pool, containing the prefab, initial size, and a key to identify the pool.
/// </summary>
[Serializable]
public class PoolConfig
{
    public string poolKey; // Unique key to identify the pool.
    public GameObject prefab; // Prefab to instantiate and pool.
    public int initialSize; // Initial number of objects in the pool.
}

/// <summary>
/// Manages multiple object pools, allowing retrieval and returning of objects by pool key.
/// </summary>
public class ObjectPoolManager : MonoBehaviour
{
    [SerializeField]
    private List<PoolConfig> poolsConfig; // List of pool configurations.

    private Dictionary<string, object> pools = new Dictionary<string, object>(); // Dictionary of object pools.

    private void Awake()
    {
        InitializePools();
    }

    /// <summary>
    /// Initializes all pools based on the configuration provided in the inspector.
    /// </summary>
    private void InitializePools()
    {
        foreach (var config in poolsConfig)
        {
            var poolType = typeof(ObjectPool<>).MakeGenericType(config.prefab.GetComponent<MonoBehaviour>().GetType());
            var pool = Activator.CreateInstance(poolType, config.prefab, config.initialSize);
            pools.Add(config.poolKey, pool);
        }
    }

    /// <summary>
    /// Retrieves an object from the pool identified by the specified key.
    /// </summary>
    /// <typeparam name="T">The type of object to retrieve.</typeparam>
    /// <param name="poolKey">The key identifying the pool.</param>
    /// <returns>An object from the specified pool.</returns>
    public T GetObject<T>(string poolKey) where T : MonoBehaviour
    {
        if (pools.TryGetValue(poolKey, out var pool))
        {
            return ((ObjectPool<T>)pool).Spawn();
        }

        Debug.LogError($"Pool with key {poolKey} not found.");
        return null;
    }

    /// <summary>
    /// Returns an object to the pool identified by the specified key.
    /// </summary>
    /// <typeparam name="T">The type of object to return.</typeparam>
    /// <param name="poolKey">The key identifying the pool.</param>
    /// <param name="obj">The object to return to the pool.</param>
    public void ReturnObject<T>(string poolKey, T obj) where T : MonoBehaviour
    {
        if (pools.TryGetValue(poolKey, out var pool))
        {
            ((ObjectPool<T>)pool).Recycle(obj);
        }
        else
        {
            Debug.LogError($"Pool with key {poolKey} not found.");
        }
    }
}
