using System.Collections.Generic;
using UnityEngine;

namespace Features.Combat
{
    [CreateAssetMenu(fileName = "SO_EnemyConfig", menuName = "SO/Features/Combat/EnemyConfig")] 
    public class EnemyConfigSO : CombatConfigSO
    {
        [SerializeField] private EnemyType enemyType;
        [SerializeField] private float separationWeight;
        [SerializeField] private float separationRadious;
        [SerializeField] private List<AttackConfigSO> attackConfigs;
        

        public EnemyType EnemyType => enemyType;
        public float SeparationWeight => separationWeight;
        public float SeparationRadious => separationRadious;
        public List<AttackConfigSO> AttackConfigs => attackConfigs;  
    }
}

