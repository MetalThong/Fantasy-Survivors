using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponComponent
{
    private List<Weapon> weapons;
    public WeaponComponent(List<Weapon> weapons)
    {
        this.weapons = weapons;
    }

    public void Tick(List<Enemy> _nearbyEnemies)
    {
        foreach (var weapon in weapons)
        {
            weapon?.Tick(_nearbyEnemies);
        }
    }
    public Weapon GetWeaponAtIndex(int index)
    {
        return weapons[index];
    }
}
