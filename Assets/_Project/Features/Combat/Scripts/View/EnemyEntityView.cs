using System;
using System.Collections;
using System.Collections.Generic;
using Core.Foundation.Events;
using Core.Foundation.FSM;
using UnityEngine;

namespace Features.Combat
{
    public class EnemyEntityView : MonoBehaviour, IFsmDebugView
    {
        public IEventBus EventBus { get; private set; }
        public EnemyEntity Enemy { get; private set; }
        
        public GameObject Owner => gameObject;
        public Type ContextType => typeof(EnemyEntity);
        public IReadOnlyCollection<Type> History => Enemy.History;
        public string Transitions => Enemy.Transitions;
        
        [SerializeField] private Animator animator;
        [SerializeField] private Animator skillAnimator;

        public void OnEnable()
        {
            FsmDebugRegistry.Register(this);
        }

        public void OnDisable()
        {
            FsmDebugRegistry.UnRegister(this);
        }

        public void Initialize(EnemyEntity entity, IEventBus eventBus)
        {
            EventBus = eventBus;
            EventBus.Subscribe<StateChangedPayload>(OnStateChanged);
            EventBus.Subscribe<TriggerHitboxPayload>(OnExecuteSkill);

            Enemy = entity;
            Enemy.SetCurrentPosition(transform.position);
        }

        private void Update()
        {
            if (Input.anyKeyDown)
            {
                Enemy.TakeDamage(10000);
            }

            transform.position = Enemy.CurrentPosition;
            transform.localEulerAngles = Enemy.CurrentRotation;
        }

        private void OnStateChanged(StateChangedPayload payload)
        {
            if (payload.Source != Enemy) 
            {
                return;
            }
            animator.SetTrigger(payload.EntityAnimation);
            if (payload.CurrentState is not AttackState)
            {
                skillAnimator.gameObject.SetActive(false);
            }
        }

        private void OnExecuteSkill(TriggerHitboxPayload payload)
        {
            if (payload.Source != Enemy)
            {
                return;
            }

            skillAnimator.gameObject.SetActive(true);
            skillAnimator.SetTrigger(payload.SkillAnimation.ToString());
        }

        private void OnDestroy()
        {
            EventBus.Unsubscribe<StateChangedPayload>(OnStateChanged);
            EventBus.Unsubscribe<TriggerHitboxPayload>(OnExecuteSkill);
            Enemy.Cleanup();
        }
    }
}

