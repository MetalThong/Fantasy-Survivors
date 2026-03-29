using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponInfoSO info;
    public WeaponInfoSO Info => info;

    [SerializeField] AWeaponBehaviour behaviour;
    float coolDownLeft;
    private void Start()
    {
        coolDownLeft = 0f;
    }
    public void Tick(List<Enemy> enemies)
    {
        coolDownLeft -= Time.deltaTime;
        if (coolDownLeft <= 0)
        {
            if (enemies == null || enemies.Count == 0) return;
            if (!behaviour.Attack(enemies, info)) return;
            coolDownLeft = info.AttackCooldown;
        }
    }

}
