using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_EnemyStats", menuName = "SO/Enemy/EnemyStats")]
public class EnemyStatsSO : ScriptableObject
{
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private float maxHealth;
    [SerializeField] private float armor;
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float damage;
    [SerializeField] private float cooldown;
    [SerializeField] private float attackDuration;


    public EnemyType EnemyType => enemyType;
    public float MaxHealth => maxHealth;
    public float Armor => armor;
    public float Speed => speed;
    public float Range => range;
    public float Damage => damage;
    public float Cooldown => cooldown;
    public float AttackDuration => attackDuration;
}
