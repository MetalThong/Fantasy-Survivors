using UnityEngine;

namespace Features.Combat
{
    public interface IAttackSkill
    {
        float Damage { get; }
        float AttackRangeSqr { get; }

        bool CanAttack(Vector3 currentPosition, Vector3 targetPosition);
        void Execute(CombatEntity source, CombatEntity target);
        void Tick(float deltaTime);

        bool IsAttackFinished { get; }
    }
}