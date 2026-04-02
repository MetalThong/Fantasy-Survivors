using System;
using System.Collections.Generic;
using UnityEngine;

namespace Features.Combat
{
    public class AttackComponent
    {
        public int CurrentSkillIndex { get; private set; }
        public IAttackSkill CurrentSkill { get; private set; }

        private readonly List<IAttackSkill> _attackSkills;
        private readonly float _minAttackRangeSqr;
        
        
        private bool HasSkills => _attackSkills != null && _attackSkills.Count > 0;

        public AttackComponent(List<AttackConfigSO> attackConfigs, AttackSkillFactory skillFactory)
        {
            _attackSkills = new();

            if (attackConfigs == null || attackConfigs.Count == 0)
            {
                return;
            }

            foreach (AttackConfigSO attackConfig in attackConfigs)
            {
                _attackSkills.Add(skillFactory.Create(attackConfig));
            }
            
            CurrentSkill = _attackSkills.Count > 0 ? _attackSkills[0] : null;
            CurrentSkillIndex = 0;

            _minAttackRangeSqr = CurrentSkill.AttackRangeSqr;
            foreach (IAttackSkill attackSkill in _attackSkills)
            {
                _minAttackRangeSqr = Math.Min(attackSkill.AttackRangeSqr, _minAttackRangeSqr);
            }
        }

        public void Tick(float deltaTime)
        {
            if (!HasSkills)
            {
                return;
            }

            foreach (IAttackSkill attackSkill in _attackSkills)
            {
                attackSkill.Tick(deltaTime);
            }
        }

        public bool IsCurrentAttackFinished => CurrentSkill?.IsAttackFinished ?? true;

        public bool IsTargetInRange(Vector3 currentPosition, Vector3 targetPosition)
        {
            if (!HasSkills)
            {
                return false;
            }

            float distane = (targetPosition - currentPosition).sqrMagnitude;
            return distane < _minAttackRangeSqr;
        }

        public bool HasSkillReady(Vector3 currentPosition, Vector3 targetPosition)
        {

            if (!HasSkills)
            {
                return false;
            }
            foreach (IAttackSkill attackSkill in _attackSkills)
            {
                if (attackSkill.CanAttack(currentPosition, targetPosition))
                {
                    return true;
                }
            }
            
            return false;
        }

        public bool TryExecuteAttack(CombatEntity source, CombatEntity target)
        {
            if (!HasSkills || !IsCurrentAttackFinished) return false;
 
            for (int i = 0; i < _attackSkills.Count; i++)
            {
                if (_attackSkills[i].CanAttack(source.CurrentPosition, target.CurrentPosition))
                {
                    CurrentSkillIndex = i;
                    CurrentSkill = _attackSkills[i];
                    CurrentSkill.Execute(source, target);
                    return true;
                }
            }
 
            return false;
        }

        public void Reset()
        {
            CurrentSkill = _attackSkills.Count > 0 ? _attackSkills[0] : null;
        }
    }
}

