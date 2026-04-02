using UnityEngine;

namespace Features.Combat
{
    public class AttackSkillBase : IAttackSkill
    {
        public float Damage { get; private set; }
        public float AttackRangeSqr { get; private set; }
        private readonly float _attackDuration;
        private readonly float _cooldown;
        
        private float _durationTimer;
        private float _cooldownTimer;

        public bool IsAttackFinished => _durationTimer <= 0;
        private bool IsCooldownFinished => _cooldownTimer <= 0;
        
        public AttackSkillBase(AttackConfigSO attackConfig)
        {
            Damage = attackConfig.Damage;
            AttackRangeSqr = attackConfig.AttackRange * attackConfig.AttackRange;

            _attackDuration = attackConfig.AttackDuration;
            _cooldown = attackConfig.Cooldown;

            _durationTimer = 0;
            _cooldownTimer = 0;
        }

        public void Tick(float deltaTime)
        {
            _durationTimer -= deltaTime;
            _cooldownTimer -= deltaTime;
        }

        public virtual void Execute(CombatEntity source, CombatEntity target)
        {
            StartAttackDuration();
            StartCooldown();
        }

        public bool CanAttack(Vector3 currentPosition, Vector3 targetPosition)
        {
            return IsCooldownFinished && IsTargetInRange(currentPosition, targetPosition);
        }

        private bool IsTargetInRange(Vector3 currentPosition, Vector3 targetPosition)
        {
            float distance = (targetPosition - currentPosition).sqrMagnitude;
            return distance < AttackRangeSqr;
        }

        private void StartAttackDuration()
        {
            _durationTimer = _attackDuration;
        }

        private void StartCooldown()
        {
            _cooldownTimer = _cooldown;
        }
    }   
}