using UnityEngine;

namespace Features.Combat
{
    public class AttackConfigSO : ScriptableObject
    {
        [SerializeField] private float attackRange;
        [SerializeField] private float damage;
        [SerializeField] private float attackDuration;
        [SerializeField] private float cooldown;
        
        public float AttackRange => attackRange;
        public float Damage => damage;
        public float AttackDuration => attackDuration;
        public float Cooldown => cooldown;
        
    }
}