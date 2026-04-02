using UnityEngine;

namespace Features.Combat
{
    [CreateAssetMenu(fileName = "SO_HitboxAttack", menuName = "SO/Features/Combat/HitboxAttack")]
    public class HitboxAttackSO : AttackConfigSO
    {
        [SerializeField] private SkillAnimation skillAnimation;

        public SkillAnimation SkillAnimation => skillAnimation;
    }
}