using System;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    [Is(ObjectTypes.Damageable)]
    public sealed class HealthComponent : IDisposable
    {
        [SerializeField]
        private int _hitPoints;

        [Get(ObjectAPI.HitPoints)] 
        private AtomicVariable<int> _currentHitPoints;
        
        [Get(ObjectAPI.TakeDamageAction)]
        private readonly AtomicEvent<int> _takeDamageEvent = new();
        
        private readonly AtomicEvent _deathEvent = new();
        
        [SerializeField]
        private AtomicFunction<bool> _isAlive;
        
        private TakeDamageMechanics _takeDamageMechanics;
        private DeathMechanics _deathMechanics;
        
        [Get(ObjectAPI.IsAlive)]
        public IAtomicValue<bool> IsAlive => _isAlive;
        
        public IAtomicObservable<int> TakeDamageEvent => _takeDamageEvent;
        
        [Get(ObjectAPI.DeathObservable)]
        public IAtomicObservable DeathEvent => _deathEvent;
        
        public void Compose()
        {
            _currentHitPoints = new AtomicVariable<int>(_hitPoints);
            
            _isAlive.Compose(() => _currentHitPoints.Value > 0);
            
            _takeDamageMechanics = new TakeDamageMechanics(_currentHitPoints, _takeDamageEvent, _isAlive);
            _deathMechanics = new DeathMechanics(_currentHitPoints, _deathEvent);
        }
        
        public void OnEnable()
        {
            _takeDamageMechanics.OnEnable();
            _deathMechanics.OnEnable();
        }
        
        public void OnDisable()
        {
            _takeDamageMechanics.OnDisable();
            _deathMechanics.OnDisable();
        }

        public void Dispose()
        {
            _currentHitPoints?.Dispose();
            _takeDamageEvent?.Dispose();
            _deathEvent?.Dispose();
        }
    }
}