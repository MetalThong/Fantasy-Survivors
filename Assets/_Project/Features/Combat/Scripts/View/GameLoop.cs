using System.Collections;
using System.Collections.Generic;
using Reflex.Attributes;
using UnityEngine;

namespace Features.Combat
{
    public class GameLoop : MonoBehaviour
    {
        [Inject] private readonly DamageSystem _damageSystem;
        private readonly List<CombatEntity> _entities = new();

        public void Register(CombatEntity entity)
        {
            _entities.Add(entity);

        }
        
        private void Update()
        {
            foreach (var entity in _entities)
            {
                entity.Update(Time.deltaTime);
            }
        }

        private void FixedUpdate()
        {
            foreach (var entity in _entities)
            {
                entity.FixedUpdate(Time.deltaTime);
            }
        }

        private void OnDestroy()
        {
            _damageSystem?.Dispose();
        }
    }
}

