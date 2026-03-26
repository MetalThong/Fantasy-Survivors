using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackComponent
{
    public float Damage { get; private set; }
    private readonly float _range;
    private readonly float _cooldown;
    private readonly float _attackDuration;

    private float _cooldownTimer;
    private float _durationTimer;

    public AttackComponent(EnemyStatsSO statsSO)
    {
        Damage = statsSO.Damage;

        _range = statsSO.Range;
        _cooldown = statsSO.Cooldown;
        _attackDuration = statsSO.AttackDuration;

        _cooldownTimer = 0;
        _durationTimer = _attackDuration;
    }

    public bool IsCooldownReady => _cooldownTimer <= 0f;
    public bool IsAttackFinished => _durationTimer <= 0f;

    public bool IsTargetInRange(Vector3 position, Vector3 targetPosition)
    {
        float distance = Vector3.Distance(position, targetPosition);
        return distance < _range;
    }

    public void Tick(float deltaTime)
    {
        _cooldownTimer -= deltaTime;
        _durationTimer -= deltaTime;
    }

    public void StartCooldown()
    {
        _cooldownTimer = _cooldown;
    }

    public void StartAttackDuration()
    {   
        _durationTimer = _attackDuration;
    }
}
