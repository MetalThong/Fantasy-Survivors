using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WeaponInfoSO", menuName = "SO/WeaponInfoSO")]
public class WeaponInfoSO : ScriptableObject
{
    [SerializeField] private WeaponID id;
    [SerializeField] private Sprite icon;
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackCooldown;
    [SerializeField] private List<AugmentInfoSO> nextAugmentInfo;

    public WeaponID ID => id;
    public Sprite Icon => icon;
    public float AttackRange => attackRange;
    public float AttackCooldown => attackCooldown;
    public float AttackDamage => attackDamage;
    public List<AugmentInfoSO> NextAugmentInfo => nextAugmentInfo;
}
