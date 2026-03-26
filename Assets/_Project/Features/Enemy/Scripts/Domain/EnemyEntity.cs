using System;
using System.Collections;
using System.Collections.Generic;
using Core.Foundation.FSM;
using UnityEngine;

public class EnemyEntity : MonoBehaviour, IDamageable
{
    public UnitType UnitType { get; private set; }
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyStatsSO statsSO;
    [SerializeField] private EnemyRewardSO rewardSO;
    [SerializeField] private Transform target;

    private StateMachine<EnemyEntity> _stateMachine;

    private HealthComponent _healthComponent;
    private MovementComponent _movementComponent;
    private AttackComponent _attackComponent;

    public float CurrentHealth => _healthComponent.CurrentHealth;
    public Vector3 CurrentPosition => _movementComponent.CurrentPosition;

    public bool HasTarget => target != null;
    public bool IsCooldownReady => _attackComponent.IsCooldownReady;
    public bool IsTargetInRange => _attackComponent.IsTargetInRange(CurrentPosition, target.position);
    public bool IsAttackFinished => _attackComponent.IsAttackFinished;
    public bool IsDead => CurrentHealth <= 0f;

    
    public event Action<float, int, bool> OnDropReward;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        _healthComponent = new(statsSO);
        _movementComponent = new(statsSO);
        _attackComponent = new(statsSO);

        
        BuildStateMachine();
    }

    public void Update()
    {
        transform.parent.position = CurrentPosition;

        _stateMachine?.Update(Time.deltaTime);
        Debug.Log(_stateMachine.CurrentState);
    }

    public void Tick(float deltaTime)
    {
        _attackComponent.Tick(deltaTime);
    }

    public bool IsFinishedAnimation(string stateName)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName(stateName) && stateInfo.normalizedTime >= 1f;
    }

    public void TakeDamage(float damage)
    {
        _healthComponent.TakeDamage(damage);
    }

    public void MovingToTarget(float deltaTime)
    {
        if (HasTarget)
        {
            _movementComponent.MovingTo(target.position, deltaTime);
        }
    }

    public void StartCooldown()
    {
        _attackComponent.StartCooldown();
    }

    public void StartAttackDuration()
    {
        _attackComponent.StartAttackDuration();
    }

    public void OnDead()
    {
        DropReward();
    }

    private void DropReward()
    {
        float randomValue = UnityEngine.Random.Range(0, 1f);
        bool isEpicDrop = randomValue > rewardSO.EpicDropRate;

        OnDropReward?.Invoke(rewardSO.ExpReward, rewardSO.ExpAmount, isEpicDrop);   
    }

    private void BuildStateMachine()
    {
        MoveState moveState = new();
        AttackState attackState = new();
        IdleState idleState = new();
        DieState dieState = new();
        _stateMachine = StateMachineFactory<EnemyEntity>.Create(this, moveState, 5);

        _stateMachine.AddTransition(null, dieState, new IsDeadGuard());
        _stateMachine.AddTransition(moveState, attackState, new CanAttackGuard());
        _stateMachine.AddTransition(attackState, idleState, new IsAttackFinishedGuard());
        _stateMachine.AddTransition(idleState, attackState, new CanAttackGuard());
        _stateMachine.AddTransition(idleState, moveState, new IsTargetLostGuard());   
    }
}
