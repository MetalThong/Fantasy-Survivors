using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AWeaponBehaviour : MonoBehaviour 
{
    public abstract bool Attack(List<Enemy> target, WeaponInfoSO info);
}

