using System.Collections;
using System.Collections.Generic;
using Core.Foundation.Events;
using UnityEngine;

namespace Features.Combat
{
    public class DamageSystem
    {
        private readonly IEventBus _eventBus;

        public DamageSystem(IEventBus eventBus)
        {
            _eventBus = eventBus;
            _eventBus.Subscribe<HitConfirmedPayload>(OnHitConfirmed);
        }

        private void OnHitConfirmed(HitConfirmedPayload payload)
        {
            if (!payload.Target.IsAlive) 
            {
                return;
            }

            float damage = CalculateDamage(payload.RawDamage, payload.Target);
            payload.Target.TakeDamage(damage);

            _eventBus.Publish(new DamageDealtPayload(payload.Source, payload.Target, damage));
        }

        private float CalculateDamage(float rawDamage, CombatEntity target)
        {
            return Mathf.Max(0, rawDamage - target.Armor);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<HitConfirmedPayload>(OnHitConfirmed);
        }
    }
}