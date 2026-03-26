using System;
using System.Collections;
using System.Collections.Generic;
using Core.Foundation.Pool;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PoolRegistry : MonoBehaviour
{
    public static PoolRegistry Instance { get; private set; }

    private readonly Dictionary<Type, object> _type2IPool;
    private readonly Dictionary<Type, PoolStats> _type2PoolStats;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void RegisterPool<T>(IPool<T> pool) where T : class
    {
        Type poolType = pool.GetType();

        _type2IPool[poolType] = pool;
        _type2PoolStats[poolType] = new(poolType);
    }

    public T Get<T>(Type poolType) where T : class
    {
        if (_type2IPool.TryGetValue(poolType, out var poolObject))
        {
            if (poolObject is IPool<T> pool)
            {
                T poolableObject = pool.Get();
                UpdatePoolStats(poolType, pool, true);

                return poolableObject;
            }

            return null;
        }

        return null;
    }

    public void Release<T>(Type poolType, T poolableObject) where T : class
    {
        if (_type2IPool.TryGetValue(poolType, out var poolObject))
        {
            if (poolObject is IPool<T> pool)
            {
                pool.Release(poolableObject);
                UpdatePoolStats(poolType, pool, false);
            }
        }
    }

    private void UpdatePoolStats<T>(Type poolType, IPool<T> pool, bool isGettingFromPool) where T : class
    {
        if (_type2PoolStats.TryGetValue(poolType, out var poolStats))
        {
            poolStats.CurrentActive = pool.ActiveCount;
            poolStats.CurrentAvailable = pool.AvailableCount;

            if (poolStats.CurrentActive > poolStats.MaxActive)
            {
                poolStats.MaxActive = poolStats.CurrentActive;
            }

            if(isGettingFromPool)
            {
                poolStats.GetCount++;
            }
            else
            {
                poolStats.ReleaseCount++;
            }
        }
    }

    public PoolStats GetStats(Type poolType)
    {
        if (_type2PoolStats.TryGetValue(poolType, out var poolStats))
        {
            return poolStats;
        }

        return null;
    }
}
