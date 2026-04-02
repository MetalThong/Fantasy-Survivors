namespace Features.Combat
{
    public struct HitConfirmedPayload
    {
        public CombatEntity Source;
        public CombatEntity Target;
        public float RawDamage;
        
        public HitConfirmedPayload(CombatEntity source, CombatEntity target, float rawDamage)
        {
            Source = source;
            Target = target;
            RawDamage = rawDamage;
        }
    }
}