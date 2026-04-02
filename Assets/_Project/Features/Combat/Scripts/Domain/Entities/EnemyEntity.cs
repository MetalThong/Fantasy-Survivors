using System;
using System.Collections.Generic;
using Core.Foundation.FSM;
using UnityEngine;
using Core.Foundation.Events;

namespace Features.Combat
{
    public class EnemyEntity : CombatEntity
    {
        public CombatEntity CurrentTarget { get; private set; }

        private readonly List<CombatEntity> _neighbors = new();
        private readonly INeighborProvider _neighborProvider;
        private readonly SteeringComponent _steeringComponent;
        private readonly AttackComponent _attackComponent;
        private readonly IEventBus _eventBus;
        private StateMachine<EnemyEntity> _stateMachine;
    
        private readonly float _separationRadious;


        public bool HasSkillReady => _attackComponent.HasSkillReady(CurrentPosition, CurrentTarget.CurrentPosition);
        public bool HasTargetInRange => CurrentTarget != null && _attackComponent.IsTargetInRange(CurrentPosition, CurrentTarget.CurrentPosition);
        public IAttackSkill CurrentSkill => _attackComponent.CurrentSkill;
        public bool IsAttackFinished => _attackComponent.IsCurrentAttackFinished;


        public IState<EnemyEntity> CurrentState => _stateMachine.CurrentState;
        public IReadOnlyCollection<Type> History => _stateMachine.History.StateTypes;
        public string Transitions => _stateMachine.GetTransitionsDebugString();

        public EnemyEntity(EnemyConfigSO enemyConfig, INeighborProvider neighborProvider, AttackSkillFactory skillFactory, IEventBus eventBus) : base(enemyConfig)
        {
            _separationRadious = enemyConfig.SeparationRadious;
            _neighborProvider = neighborProvider;
            _steeringComponent = new(new SeekBehavior(), new SeparationBehavior(enemyConfig.SeparationWeight));
            _attackComponent = new(enemyConfig.AttackConfigs, skillFactory);
            _eventBus = eventBus;

            BuildStateMachine();
        }

        private void BuildStateMachine()
        {
            MoveState moveState = new();
            AttackState attackState = new();
            IdleState idleState = new();
            DieState dieState = new();

            _stateMachine = StateMachineFactory<EnemyEntity>.Create(this, moveState);
            _stateMachine.AddTransition(null, dieState, new IsDeadGuard());

            _stateMachine.AddTransition(moveState, attackState, new HasSkillReadyGuard());
            _stateMachine.AddTransition(attackState, idleState, new IsAttackFinishedGuard());
            _stateMachine.AddTransition(idleState, attackState, new HasSkillReadyGuard());

            _stateMachine.AddTransition(idleState, moveState, new LostTargetGuard());
            _stateMachine.AddTransition(moveState, idleState, new HasTargetInRangeGuard());

            _stateMachine.OnStateChanged += HandleStateChanged;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            _attackComponent.Tick(deltaTime);
            _stateMachine.Update(deltaTime);
        }

        public override void FixedUpdate(float fixedDeltaTime)
        {
            base.FixedUpdate(fixedDeltaTime);

            _stateMachine.FixedUpdate(fixedDeltaTime);
        }

        public void MoveToTarget(float fixedDeltaTime)
        {
            if (CurrentTarget != null)
            {
                _neighborProvider.CollectNeighbors(_neighbors, CurrentPosition, _separationRadious);

                SteeringContext context = new(CurrentPosition, CurrentTarget.CurrentPosition, _neighbors); 
                Vector3 direction = _steeringComponent.CalculateTotalDirection(context);

                _movementComponent.MoveTo(direction, fixedDeltaTime);
            }
        }

        public void HandleStateChanged()
        {
            string entityAnimation = _stateMachine.CurrentState switch
            {
                MoveState => EntityAnimation.move.ToString(),
                AttackState => EntityAnimation.attack.ToString() + _attackComponent.CurrentSkillIndex,
                IdleState => EntityAnimation.idle.ToString(),
                DieState => EntityAnimation.die.ToString(),
                _ => EntityAnimation.idle.ToString()
            };
            _eventBus.Publish(new StateChangedPayload(this, CurrentState, entityAnimation));
        }

        public bool TryExecuteAttack()
        {
            return _attackComponent.TryExecuteAttack(this, CurrentTarget);
        }

        public void SetTarget(CombatEntity target)
        {
            CurrentTarget = target;
        }

        public override void Reset(Vector3 newPosition)
        {
            base.Reset(newPosition);
            _attackComponent.Reset();

            _stateMachine.OnStateChanged -= HandleStateChanged;
            _stateMachine.Reset();
            _stateMachine.OnStateChanged += HandleStateChanged;
        }

        public void Cleanup()
        {
            _stateMachine.OnStateChanged -= HandleStateChanged;
        }
    }
}