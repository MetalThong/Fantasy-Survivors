using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatComponent 
{
    private float MaxHealth;
    private float CurrentHealth;
    private float PickupRange;
    public StatComponent(float maxHealth, float currentHealth, float pickupRange)
    {
        MaxHealth = maxHealth;
        CurrentHealth = currentHealth;
        PickupRange = pickupRange;
    }

    public void IncreaseMaxHealth(float value)
    {
        MaxHealth += value;
    }
    public void IncreaseCurrentHealth(float value)
    {
        CurrentHealth += Mathf.Clamp(CurrentHealth + value, 0, MaxHealth);
    }
    public void IncreasePickUpRange(float value)
    {
        PickupRange += value;
    }
    public void TakeDamage(float value)
    {
        CurrentHealth -= value;
    }
    public float GetMaxHealth() { return MaxHealth; }
    public float GetCurrentHealth() {  return CurrentHealth; }
    public float GetPickupRange() {  return PickupRange; }

}
