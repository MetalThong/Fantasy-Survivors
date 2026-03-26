using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    UnitType UnitType { get; }
    void TakeDamage(float damage);
}
