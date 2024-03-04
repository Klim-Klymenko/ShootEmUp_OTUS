using System;
using Atomic.Elements;
using Atomic.Objects;
using GameEngine;
using UnityEngine;
using UnityEngine.Serialization;

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
        private NavMeshAgentComponent _agentComponent;
        
        [Section]
        [SerializeField] 
        private CooldownAttackComponent _cooldownAttackComponent;
        
        internal IAtomicValue<bool> MoveCondition => _agentComponent.MoveCondition;
        internal IAtomicObservable AttackRequestEvent => _cooldownAttackComponent.AttackRequestEvent;
        internal IAtomicObservable AttackEvent => _cooldownAttackComponent.AttackEvent;
        internal IAtomicObservable DeathEvent => _healthComponent.DeathObservable;
        
        internal void Compose()
        {
            _healthComponent.Compose();
            _agentComponent.Compose(_healthComponent.AliveCondition);
            _cooldownAttackComponent.Compose(_healthComponent.AliveCondition, _transform);
        }
        
        internal void OnEnable()
        {
            _healthComponent.OnEnable();
            _cooldownAttackComponent.OnEnable();
            _agentComponent.OnEnable();
        }
        
        internal void Update()
        {
            _agentComponent.Update();
            _cooldownAttackComponent.Update();
        }

        internal void OnDisable()
        {
            _healthComponent.OnDisable();
            _cooldownAttackComponent.OnDisable();
            _agentComponent.OnDisable();
        }
        
        public void Dispose()
        {
            _healthComponent?.Dispose();
            _agentComponent?.Dispose();
            _cooldownAttackComponent?.Dispose();
        }
    }
}