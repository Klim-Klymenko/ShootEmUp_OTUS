using System;
using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using GameEngine;
using UnityEngine;

namespace Objects
{
    [Serializable]
    internal sealed class Zombie_Core : IDisposable
    {
        [SerializeField]
        private Transform _transform;
        
        [Section]
        [SerializeField] 
        private HealthComponent _healthComponent;
        
        [Section]
        [SerializeField] 
        private CooldownAttackComponent _cooldownAttackComponent;
        
        internal IAtomicValue<bool> AliveCondition => _healthComponent.AliveCondition;
        internal IAtomicObservable AttackRequestEvent => _cooldownAttackComponent.AttackRequestEvent;
        internal IAtomicObservable AttackEvent => _cooldownAttackComponent.AttackEvent;
        internal IAtomicObservable DeathEvent => _healthComponent.DeathObservable;
        
        internal void Compose()
        {
            _healthComponent.Compose();

            _cooldownAttackComponent.Let(it =>
            {
                it.Compose(_transform);
                it.AttackCondition.Append(AliveCondition);
            });
        }
        
        internal void OnEnable()
        {
            _healthComponent.OnEnable();
            _cooldownAttackComponent.OnEnable();
        }
        
        internal void Update()
        {
            _cooldownAttackComponent.Update();
        }

        internal void OnDisable()
        {
            _healthComponent.OnDisable();
            _cooldownAttackComponent.OnDisable();
        }
        
        public void Dispose()
        {
            _healthComponent?.Dispose();
            _cooldownAttackComponent?.Dispose();
        }
    }
}