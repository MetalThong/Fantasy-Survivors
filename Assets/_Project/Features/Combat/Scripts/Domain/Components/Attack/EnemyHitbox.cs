using System.Collections;
using System.Collections.Generic;
using Core.Foundation.Events;
using Features.Combat;
using Reflex.Attributes;
using UnityEngine;

namespace Features.Combat
{
    public class EnemyHitbox : MonoBehaviour
    {
        [SerializeField] private EnemyEntityView _view;

        private void OnTriggerEnter2D(Collider2D collider)
        {       
            if (collider.TryGetComponent<EnemyEntityView>(out var targetView))
            {
                // Debug.Log($"Enemy = {_view.Enemy}, Target = {targetView.Enemy}, Damage = {_view.Enemy.CurrentSkill.Damage}");
                _view.EventBus.Publish(new HitConfirmedPayload(_view.Enemy, targetView.Enemy, _view.Enemy.CurrentSkill.Damage));
            }
        }
    }
}