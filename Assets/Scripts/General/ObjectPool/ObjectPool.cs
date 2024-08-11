using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generic Object Pool for managing reusable objects.
/// </summary>
/// <typeparam name="T">The type of objects to pool.</typeparam>
public class ObjectPool<T> where T : MonoBehaviour
{
    private readonly T prefab;
    private readonly Stack<T> pool;

    /// <summary>
    /// Initializes a new instance of the ObjectPool class.
    /// </summary>
    /// <param name="prefab">The prefab to pool.</param>
    /// <param name="initialSize">The initial size of the pool.</param>
    public ObjectPool(T prefab, int initialSize)
    {
        this.prefab = prefab;
        pool = new Stack<T>(initialSize);

        for (int i = 0; i < initialSize; i++)
        {
            T obj = CreateNewObject();
            pool.Push(obj);
        }
    }

    /// <summary>
    /// Spawns an object from the pool.
    /// </summary>
    /// <returns>The spawned object.</returns>
    public T Spawn()
    {
        T obj;

        if (pool.Count > 0)
        {
            obj = pool.Pop();
            obj.gameObject.SetActive(true);
        }
        else
        {
            obj = CreateNewObject();
        }

        return obj;
    }

    /// <summary>
    /// Recycles the object back into the pool.
    /// </summary>
    /// <param name="obj">The object to recycle.</param>
    public void Recycle(T obj)
    {
        obj.gameObject.SetActive(false);
        pool.Push(obj);
    }

    /// <summary>
    /// Creates a new object instance and adds it to the pool.
    /// </summary>
    /// <returns>The created object.</returns>
    private T CreateNewObject()
    {
        T newObj = Object.Instantiate(prefab);
        newObj.gameObject.SetActive(false);
        return newObj;
    }
}
