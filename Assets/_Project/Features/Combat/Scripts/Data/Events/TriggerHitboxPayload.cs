namespace Features.Combat
{
    public struct TriggerHitboxPayload
    {
        public CombatEntity Source;
        public SkillAnimation SkillAnimation;

        public TriggerHitboxPayload(CombatEntity source, SkillAnimation skillAnimation)
        {
            Source = source;
            SkillAnimation = skillAnimation;
        }
    }   
}