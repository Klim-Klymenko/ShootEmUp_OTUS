using System;
using Atomic.Elements;
using Atomic.Objects;
using GameEngine;
using UnityEngine;
using Zenject;

namespace Objects
{
    [Serializable]
    public sealed class Character_Core : IDisposable
    {
        [Section]
        [SerializeField]
        private HealthComponent _healthComponent;
        
        [Section]
        [SerializeField]
        private MoveComponent _moveComponent;

        [Section]
        [SerializeField] 
        private ShootComponent _shootComponent;

        [SerializeField]
        private ReplenishComponent _replenishComponent;
        
        private FinishGameComponent _finishGameComponent = new();
        
        public IAtomicObservable<int> TakeDamageEvent => _healthComponent.TakeDamageEvent;
        public IAtomicObservable DeathEvent => _healthComponent.DeathEvent;
        public IAtomicObservable ShootEvent => _shootComponent.ShootEvent;
        public IAtomicValue<bool> MoveCondition => _moveComponent.MoveCondition;
        public IAtomicValue<bool> IsAlive => _healthComponent.IsAlive;
        
        public void Compose(DiContainer diContainer)
        {
            _healthComponent.Compose();
            _moveComponent.Compose(IsAlive);
            _shootComponent.Compose(diContainer, IsAlive);
            _replenishComponent.Compose(_shootComponent.Charges, IsAlive);
            _finishGameComponent.Compose(diContainer, IsAlive, TakeDamageEvent);
        }
        
        public void OnEnable()
        {
            _healthComponent.OnEnable();
            _finishGameComponent.OnEnable();
            _replenishComponent.OnEnable();
        }
        
        public void Update()
        {
            _moveComponent.Update();
            _replenishComponent.Update();
        }
        
        public void OnDisable()
        {
            _healthComponent.OnDisable();
            _finishGameComponent.OnDisable();
            _replenishComponent.OnDisable();
        }
        
        public void Dispose()
        {
            _healthComponent?.Dispose();
            _moveComponent?.Dispose();
            _shootComponent?.Dispose();
            _replenishComponent?.Dispose();
        }
    }
}