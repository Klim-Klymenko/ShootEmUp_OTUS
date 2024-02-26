using System;
using Atomic.Elements;
using Atomic.Objects;
using GameEngine;
using UnityEngine;

namespace Objects
{
    [Serializable]
    public sealed class Zombie_Core : IDisposable
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
        
        public IAtomicValue<bool> MoveCondition => _agentComponent.MoveCondition;
        public IAtomicObservable AttackRequestEvent => _attackComponent.AttackRequestEvent;
        public IAtomicObservable AttackEvent => _attackComponent.AttackEvent;
        public IAtomicObservable DeathEvent => _healthComponent.DeathEvent;
        
        public void Compose()
        {
            _healthComponent.Compose();
            _agentComponent.Compose(_healthComponent.IsAlive);
            _attackComponent.Compose(_healthComponent.IsAlive, _transform);
        }
        
        public void OnEnable()
        {
            _healthComponent.OnEnable();
            _attackComponent.OnEnable();
            _agentComponent.OnEnable();
        }
        
        public void Update()
        {
            _agentComponent.Update();
            _attackComponent.Update();
        }

        public void OnDisable()
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