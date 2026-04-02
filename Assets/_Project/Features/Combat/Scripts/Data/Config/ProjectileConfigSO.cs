using UnityEngine;

namespace Features.Combat
{
    [CreateAssetMenu(fileName = "SO_ProjectileConfig", menuName = "SO/Features/Combat/ProjectileConfig")] 
    public class ProjectileConfigSO : ScriptableObject
    {
        [SerializeField] private int amount;
        [SerializeField] private float speed;
        [SerializeField] private bool isPiercing;
        [SerializeField] private bool isFocus;
        [SerializeField] private bool isBoomerang;

        public int Amount => amount;
        public float Speed => speed;
        public bool IsPiercing => isPiercing;
        public bool IsFocus => isFocus;
        public bool IsBoomerang => isBoomerang; 
    }
}

