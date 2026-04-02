using Core.Foundation.Events;

namespace Features.Combat
{
    public class ProjectileSkill : AttackSkillBase
    {
        private readonly ProjectileConfigSO _projectileConfig;
        private readonly IEventBus _eventBus;

        public ProjectileSkill(ProjectileAttackSO projectileAttack, IEventBus eventBus) : base(projectileAttack)
        {
            _projectileConfig = projectileAttack.ProjectileConfig;
            _eventBus = eventBus;
        }

        public override void Execute(CombatEntity source, CombatEntity target)
        {
            base.Execute(source, target);

            _eventBus.Publish(new SpawnProjectilePayload(source, target, _projectileConfig));
        }
    }
}

