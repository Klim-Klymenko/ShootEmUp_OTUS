using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    public sealed class TakeDamageMechanics
    {
        private const int MinHitPoints = 0;

        private readonly IAtomicVariable<int> _hitPoints;
        private readonly IAtomicObservable<int> _takeDamageEvent;
        private readonly IAtomicValue<bool> _isAlive;

        public TakeDamageMechanics(IAtomicVariable<int> hitPoints, IAtomicObservable<int> takeDamageEvent, IAtomicValue<bool> isAlive)
        {
            _takeDamageEvent = takeDamageEvent;
            _hitPoints = hitPoints;
            _isAlive = isAlive;
        }

        public void OnEnable()
        {
            _takeDamageEvent.Subscribe(TakeDamage);
        }
        
        public void OnDisable()
        {
            _takeDamageEvent.Unsubscribe(TakeDamage); 
        }
        
        private void TakeDamage(int damage)
        {
            if (!_isAlive.Value) return;
            
            _hitPoints.Value = Mathf.Max(MinHitPoints, _hitPoints.Value - damage);
        }
    }
}