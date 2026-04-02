namespace Features.Combat
{
    public struct SpawnProjectilePayload
    {
        public CombatEntity Source;
        public CombatEntity Target;
        public ProjectileConfigSO ProjectileConfig;

        public SpawnProjectilePayload(CombatEntity source, CombatEntity target, ProjectileConfigSO projectileConfig)
        {
            Source = source;
            Target = target;
            ProjectileConfig = projectileConfig;
        }
    }   
}