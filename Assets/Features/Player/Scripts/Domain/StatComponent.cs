using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatComponent
{
    private float MaxHealth { get; set; }
    private float CurrentHealth { get; set; }
    private float PickupRange { get; set; }
    public StatComponent(float maxHealth, float currentHealth, float pickupRange)
    {
        MaxHealth = maxHealth;
        CurrentHealth = currentHealth;
        PickupRange = pickupRange;
    }
}
