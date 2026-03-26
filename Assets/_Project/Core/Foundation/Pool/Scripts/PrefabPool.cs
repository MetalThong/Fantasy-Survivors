
using UnityEngine;
using UnityEngine.Pool;

namespace Core.Foundation.Pool
{
    public class PrefabPool<T> : IPool<T> where T : MonoBehaviour, IPoolable
    {
        private readonly T _prefab;
        private readonly Transform _parent;
        private readonly ObjectPool<T> _pool;
        
        public PrefabPool(T prefab, Transform parent, int defaultCapacity = 10, int maxSize = 50, bool collectionCheck = false)
        {
            _prefab = prefab;
            _parent = parent;

            _pool = new(
                createFunc: CreateFunc,
                actionOnGet: OnGet,
                actionOnRelease: OnRelease,
                actionOnDestroy: OnDestroy,
                defaultCapacity: defaultCapacity,
                maxSize: maxSize,
                collectionCheck: collectionCheck
            );
        }

        public int ActiveCount => _pool.CountActive;
        public int AvailableCount => _pool.CountInactive;

        public T Get()
        {
            return _pool.Get();
        }

        public void Release(T poolableObject)
        {
            _pool.Release(poolableObject);
        }

        private T CreateFunc()
        {
            T poolableObject = Object.Instantiate(_prefab, _parent);
            poolableObject.gameObject.SetActive(false);
            
            return poolableObject;
        }

        private void OnGet(T poolableObject)
        {
            poolableObject.OnSpawn();
            poolableObject.gameObject.SetActive(true);
        }

        private void OnRelease(T poolableObject)
        {
            poolableObject.OnDespawn();
            poolableObject.gameObject.SetActive(false);
        }

        private void OnDestroy(T poolableObject)
        {
            poolableObject.OnDestroyed();
            
            if (poolableObject != null)
            {
                Object.Destroy(poolableObject);
            }
        }
    }
}

