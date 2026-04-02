using System;

namespace Features.Combat
{
    public class HealthComponent
    {
        public float CurrentHealth { get; private set; }
        private readonly float _maxHealth;

        public HealthComponent(float maxHealth)
        {
            _maxHealth = maxHealth;
            CurrentHealth = maxHealth;
        }

        public void TakeDamge(float damage)
        {
            if (damage > 0)
            {
                CurrentHealth = Math.Max(CurrentHealth - damage, 0);
            }
        }

        public void TakeHeal(float heal)
        {
            if (heal > 0)
            {
                CurrentHealth = Math.Min(CurrentHealth + heal, _maxHealth);
            }
        }

        public void Reset()
        {
            CurrentHealth = _maxHealth;
        }
    }
}

