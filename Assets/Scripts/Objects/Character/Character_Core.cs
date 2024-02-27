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
        [SerializeField]
        private Transform _transform;
        
        [Section]
        [SerializeField]
        private HealthComponent _healthComponent;

        [SerializeField] 
        private RaycastComponent _raycastComponent;

        [Section]
        [SerializeField]
        private MoveComponent _moveComponent;

        [SerializeField]
        private RotationComponent _rotationComponent;

        [Section]
        [SerializeField] 
        private ShootComponent _shootComponent;

        [SerializeField]
        private ReplenishComponent _replenishComponent;
        
        private FinishGameComponent _finishGameComponent = new();
        
        public IAtomicObservable<int> TakeDamageObservable => _healthComponent.TakeDamageObservable;
        public IAtomicObservable DeathObservable => _healthComponent.DeathObservable;
        public IAtomicObservable ShootObservable => _shootComponent.ShootObservable;
        public IAtomicValue<bool> MoveCondition => _moveComponent.MoveCondition;
        public IAtomicValue<bool> AliveCondition => _healthComponent.AliveCondition;
        
        public void Compose(DiContainer diContainer)
        {
            _healthComponent.Compose();
            _raycastComponent.Compose(new AtomicFunction<Vector3>(() => Input.mousePosition));
            _moveComponent.Compose(_transform, AliveCondition);
            _rotationComponent.Compose(_transform, AliveCondition, _raycastComponent.CastedPosition);
            _shootComponent.Compose(diContainer, AliveCondition);
            _replenishComponent.Compose(_shootComponent.Charges, AliveCondition);
            _finishGameComponent.Compose(diContainer, AliveCondition, TakeDamageObservable);
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
            _rotationComponent.Update();
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
            _rotationComponent?.Dispose();
            _shootComponent?.Dispose();
            _replenishComponent?.Dispose();
        }
    }
}