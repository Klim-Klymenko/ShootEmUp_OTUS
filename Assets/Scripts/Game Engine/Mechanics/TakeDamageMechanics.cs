using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    public sealed class TakeDamageMechanics
    {
        private const int MinHitPoints = 0;

        private readonly IAtomicVariable<int> _hitPoints;
        private readonly IAtomicObservable<int> _takeDamageObservable;
        private readonly IAtomicValue<bool> _aliveCondition;

        public TakeDamageMechanics(IAtomicVariable<int> hitPoints, IAtomicObservable<int> takeDamageObservable, IAtomicValue<bool> aliveCondition)
        {
            _takeDamageObservable = takeDamageObservable;
            _hitPoints = hitPoints;
            _aliveCondition = aliveCondition;
        }

        public void OnEnable()
        {
            _takeDamageObservable.Subscribe(TakeDamage);
        }
        
        public void OnDisable()
        {
            _takeDamageObservable.Unsubscribe(TakeDamage); 
        }
        
        private void TakeDamage(int damage)
        {
            if (!_aliveCondition.Value) return;
            
            _hitPoints.Value = Mathf.Max(MinHitPoints, _hitPoints.Value - damage);
        }
    }
}