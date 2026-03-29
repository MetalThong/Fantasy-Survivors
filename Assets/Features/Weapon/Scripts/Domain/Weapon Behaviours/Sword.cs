using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : AWeaponBehaviour
{
    [SerializeField] GameObject vfx;
    public override bool Attack(List<Enemy> target, WeaponInfoSO info)
    {
        Enemy closest = TargettingStrategy.FindClosestEnemyInRange(target, transform, info.AttackRange);
        if (closest != null)
        {
            Instantiate(vfx, closest.transform);
            return true;
        }
        return false;
    }
}
