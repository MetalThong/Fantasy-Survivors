using Core.Foundation.Events;

namespace Features.Combat
{
    public class HitboxSkill : AttackSkillBase
    {
        private readonly SkillAnimation _skillAnimation;
        private readonly IEventBus _eventBus;
        public HitboxSkill(HitboxAttackSO hitBoxAttack, IEventBus eventBus) : base(hitBoxAttack)
        {
            _skillAnimation = hitBoxAttack.SkillAnimation;
            _eventBus = eventBus;
        }

        public override void Execute(CombatEntity source, CombatEntity target)
        {
            base.Execute(source, target);
            _eventBus.Publish(new TriggerHitboxPayload(source, _skillAnimation));
        }
    }
}
