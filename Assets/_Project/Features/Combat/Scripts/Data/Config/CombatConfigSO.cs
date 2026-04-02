using UnityEngine;

namespace Features.Combat
{
    public abstract class CombatConfigSO : ScriptableObject
    {
        [SerializeField] private UnitType unitType;
        [SerializeField] private float maxHealth;
        [SerializeField] private float armor;
        [SerializeField] private float speed;


        public UnitType UnitType => unitType;
        public float MaxHealth => maxHealth;
        public float Armor => armor;
        public float Speed => speed; 
    }
}
