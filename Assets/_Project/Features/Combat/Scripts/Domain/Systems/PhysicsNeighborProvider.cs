using System.Collections.Generic;
using UnityEngine;

namespace Features.Combat
{
    public class PhysicsNeighborProvider : INeighborProvider
    {
        private readonly Collider[] _buffer;

        public PhysicsNeighborProvider(int bufferSize = 32)
        {
            _buffer = new Collider[bufferSize];
        }

        public void CollectNeighbors(List<CombatEntity> results, Vector3 position, float radius)
        {
            results.Clear();
            int count = Physics.OverlapSphereNonAlloc(position, radius, _buffer);

            if (count == _buffer.Length)
            {
                Debug.LogWarning($"NeighborProvider buffer full! Consider increasing size.");
            }

            for (int i = 0; i < count; i++)
            {
                if (_buffer[i].TryGetComponent<EnemyEntityView>(out var view))      
                {
                    results.Add(view.Enemy);
                }
            }
        }
    }
}

