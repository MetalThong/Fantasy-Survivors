using Core.Foundation.Events;

namespace Features.Combat
{
    public class EnemyEntityFactory
    {
        private readonly INeighborProvider _neighborProvider;
        private readonly AttackSkillFactory _skillFactory;
        private readonly IEventBus _eventBus;

        public EnemyEntityFactory(INeighborProvider neighborProvider, AttackSkillFactory skillFactory, IEventBus eventBus)
        {
            _neighborProvider = neighborProvider;
            _skillFactory = skillFactory;
            _eventBus = eventBus;
        }

        public EnemyEntity Create(EnemyConfigSO config)
        {
            return new EnemyEntity(config, _neighborProvider, _skillFactory, _eventBus);
        }
    }
}