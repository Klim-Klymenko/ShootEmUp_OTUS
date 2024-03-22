using System;
using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using GameEngine;
using UnityEngine;

namespace Objects
{
    [Serializable]
    internal sealed class Character_Core : IDisposable
    {
        [SerializeField]
        private Transform _transform;
        
        [SerializeField]
        private Gun _currentGun;
        
        [Section]
        [SerializeField]
        private HealthComponent _healthComponent;
        
        [Section]
        [SerializeField]
        private MoveComponent _moveComponent;

        [Section]
        [SerializeField]
        private RotationComponent _rotationComponent;
        
        internal IAtomicObservable<int> TakeDamageObservable => _healthComponent.TakeDamageObservable;
        internal IAtomicObservable DeathObservable => _healthComponent.DeathObservable;
        internal IAtomicObservable ShootObservable => _currentGun.ShootObservable;
        internal IAtomicValue<bool> MoveCondition => _moveComponent.MoveCondition;
        internal IAtomicValue<bool> AliveCondition => _healthComponent.AliveCondition;
        
        internal void Compose()
        {
            _healthComponent.Compose();
            
            _moveComponent.Let(it =>
            {
                it.Compose(_transform);
                it.MoveCondition.Append(AliveCondition);
            });

            _rotationComponent.Let(it =>
            {
                it.Compose(_transform);
                it.RotationCondition.Append(AliveCondition);
            });
        }
        
        internal void OnEnable()
        {
            _healthComponent.OnEnable();
        }
        
        internal void Update()
        {
            _moveComponent.Update();
            _rotationComponent.Update();
        }
        
        internal void OnDisable()
        {
            _healthComponent.OnDisable();
        }
        
        public void Dispose()
        {
            _healthComponent?.Dispose();
            _moveComponent?.Dispose();
            _rotationComponent?.Dispose();
        }
    }
}