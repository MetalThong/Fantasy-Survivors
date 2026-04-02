using System;
using Core.Foundation.Events;
using UnityEngine;

namespace Features.Combat
{
    public class AttackSkillFactory
    {
        private readonly IEventBus _eventBus;

        public AttackSkillFactory(IEventBus eventBus, DamageSystem damageSystem)
        {
            _eventBus = eventBus;
        }

        public IAttackSkill Create(AttackConfigSO config)
        {
            return config switch
            {
                HitboxAttackSO hitBoxAttack => new HitboxSkill(hitBoxAttack, _eventBus),
                ProjectileAttackSO projectileAttack => new ProjectileSkill(projectileAttack, _eventBus),
                _ => throw new ArgumentException($"Unknown config type: {config.GetType()}")
            };
        }
    }
}
