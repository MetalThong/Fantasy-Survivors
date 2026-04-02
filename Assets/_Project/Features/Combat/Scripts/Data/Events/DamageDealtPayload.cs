namespace Features.Combat
{
    public struct DamageDealtPayload
    {
        public CombatEntity Source;
        public CombatEntity Target;
        public float Damage;

        public DamageDealtPayload(CombatEntity source, CombatEntity target, float damage)
        {
            Source = source;
            Target = target;
            Damage = damage;
        }
    }
}