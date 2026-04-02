using UnityEngine;

namespace Features.Combat
{
    [CreateAssetMenu(fileName = "SO_ProjectileAttack", menuName = "SO/Features/Combat/ProjectileAttack")] 
    public class ProjectileAttackSO : AttackConfigSO
    {
        [SerializeField] private ProjectileConfigSO projectileConfig;

        public ProjectileConfigSO ProjectileConfig => projectileConfig;
    }
}