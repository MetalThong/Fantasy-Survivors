using UnityEngine;

namespace Features.Combat
{
    public class CombatEntity
    {
        public UnitType UnitType { get; private set; }
        public float Armor { get; private set; }

        public float CurrentHealth => _healthComponent.CurrentHealth;
        public Vector3 CurrentPosition => _movementComponent.CurrentPosition;
        public Vector3 CurrentRotation => _movementComponent.CurrentRotation;

        protected HealthComponent _healthComponent;
        protected MovementComponent _movementComponent;

        public bool IsAlive => _healthComponent.CurrentHealth > 0;


        public CombatEntity(CombatConfigSO combatConfig)
        {
            UnitType = combatConfig.UnitType;
            Armor = combatConfig.Armor;

            _healthComponent = new HealthComponent(combatConfig.MaxHealth);
            _movementComponent = new MovementComponent(combatConfig.Speed);
        }

        public virtual void Update(float deltaTime) {}
        public virtual void FixedUpdate(float fixedDeltaTime) {}

        public void TakeDamage(float damage)
        {
            _healthComponent.TakeDamge(damage);
        }

        public void TakeHeal(float heal)
        {
            _healthComponent.TakeHeal(heal);
        }

        public void SetCurrentPosition(Vector3 newPosition)
        {
            _movementComponent.SetCurrentPosition(newPosition);
        }


        public virtual void Reset(Vector3 newPosition)
        {
            _healthComponent.Reset();
            _movementComponent.Reset(newPosition);
        }
    }
}