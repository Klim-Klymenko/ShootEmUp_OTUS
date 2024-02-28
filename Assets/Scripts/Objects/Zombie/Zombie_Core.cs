using System;
using Atomic.Elements;
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
        private NavMeshAgentComponent _agentComponent;

        [Section]
        [SerializeField] 
        private AttackComponent _attackComponent;
        
        internal IAtomicValue<bool> MoveCondition => _agentComponent.MoveCondition;
        internal IAtomicObservable AttackRequestEvent => _attackComponent.AttackRequestEvent;
        internal IAtomicObservable AttackEvent => _attackComponent.AttackEvent;
        internal IAtomicObservable DeathEvent => _healthComponent.DeathObservable;
        
        internal void Compose()
        {
            _healthComponent.Compose();
            _agentComponent.Compose(_healthComponent.AliveCondition);
            _attackComponent.Compose(_healthComponent.AliveCondition, _transform);
        }
        
        internal void OnEnable()
        {
            _healthComponent.OnEnable();
            _attackComponent.OnEnable();
            _agentComponent.OnEnable();
        }
        
        internal void Update()
        {
            _agentComponent.Update();
            _attackComponent.Update();
        }

        internal void OnDisable()
        {
            _healthComponent.OnDisable();
            _attackComponent.OnDisable();
            _agentComponent.OnDisable();
        }
        
        public void Dispose()
        {
            _healthComponent?.Dispose();
            _agentComponent?.Dispose();
            _attackComponent?.Dispose();
        }
    }
}