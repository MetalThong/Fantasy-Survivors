using Core.Foundation.Events;
using Reflex.Core;
using UnityEngine;

namespace Features.Combat
{
    public class CombatInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private int _neighborBufferSize = 32;
        [SerializeField] private GameLoop gameLoop;

        public void InstallBindings(ContainerBuilder builder)
        {
            builder.AddSingleton(new PhysicsNeighborProvider(_neighborBufferSize), typeof(INeighborProvider));
            builder.AddSingleton(gameLoop);
            
            builder.AddSingleton(container => new AttackSkillFactory(
                container.Resolve<IEventBus>(), container.Resolve<DamageSystem>()
            ), typeof(AttackSkillFactory));

            builder.AddSingleton(container => new EnemyEntityFactory(
                container.Resolve<INeighborProvider>(),
                container.Resolve<AttackSkillFactory>(),
                container.Resolve<IEventBus>()
            ), typeof(EnemyEntityFactory));

            builder.AddSingleton(container => new DamageSystem(
                container.Resolve<IEventBus>()
            ), typeof(DamageSystem));
        }
    }
}

