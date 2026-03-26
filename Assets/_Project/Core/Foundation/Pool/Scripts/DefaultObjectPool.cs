using UnityEngine.Pool;

namespace Core.Foundation.Pool
{
    public class DefaultObjectPool<T> : IPool<T> where T : class, IPoolable, new()
    {
        private readonly ObjectPool<T> _pool;

        public DefaultObjectPool(int defaultCapacity = 10, int maxSize = 50, bool collectionCheck = false)
        {
            _pool = new(
                createFunc: () => new T(),
                actionOnGet: poolableObject => poolableObject.OnSpawn(),
                actionOnRelease: poolableObject => poolableObject.OnDespawn(),
                actionOnDestroy: poolableObject => poolableObject.OnDestroyed(),
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
    }
}