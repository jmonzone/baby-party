using System;
using System.Collections.Generic;
using UnityEngine;

// script to pool game objects  (projectiles, ally objects, enemies)
// todo: add a GetNextAvailableObject method
// todo: add a resize flag to add more objects to pool, if there are no availble objects left

public class ObjectPool<T> where T : MonoBehaviour
{
    private List<T> pool = new List<T>();
    private int poolIndex = -1;

    public int Count => pool.Count;

    public T NextObject
    {
        get
        {
            poolIndex = (poolIndex + 1) % pool.Count;
            return pool[poolIndex];
        }
    }

    public ObjectPool(T prefab, int count = 5, Action<T> onObjectCreated = null)
    {
        pool = new List<T>();
        for (int i = 0; i < count; i++)
        {
            var obj = UnityEngine.Object.Instantiate(prefab);
            obj.gameObject.SetActive(false);
            obj.name = $"{typeof(T)} {i}";
            onObjectCreated?.Invoke(obj);
            pool.Add(obj);
        }
    }
}
