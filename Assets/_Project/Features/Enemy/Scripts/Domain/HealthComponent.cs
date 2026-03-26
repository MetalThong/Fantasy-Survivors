using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent
{
    private readonly float _maxHealth;
    private readonly float _armor;

    public float CurrentHealth { get; private set; }
    
    public HealthComponent(EnemyStatsSO statsSO)
    {
        _maxHealth = statsSO.MaxHealth;
        _armor = statsSO.Armor;

        CurrentHealth = statsSO.MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        float reducedDamage = (_armor > 0) ? Math.Max(damage - _armor, 0) : damage; 
        CurrentHealth = Math.Clamp(CurrentHealth - reducedDamage, 0, _maxHealth);
    }

    public void TakeHeal(float heal)
    {
        CurrentHealth = Math.Clamp(CurrentHealth + heal, 0, _maxHealth);
    }
}
