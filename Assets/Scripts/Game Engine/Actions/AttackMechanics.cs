using System;
using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;

namespace GameEngine
{
    public sealed class AttackMechanics
    {
        private readonly IAtomicObservable _attackEvent;
        private readonly IAtomicValue<bool> _attackCondition;
        private readonly IAtomicValue<int> _damage;
        private readonly IAtomicValue<AtomicObject> _target;

        public AttackMechanics(IAtomicObservable attackEvent, IAtomicValue<bool> attackCondition, IAtomicValue<int> damage,
            IAtomicValue<AtomicObject> target)
        {
            _attackEvent = attackEvent;
            _attackCondition = attackCondition;
            _damage = damage;
            _target = target;
        }

        public void OnEnable()
        {
            _attackEvent.Subscribe(OnAttack);
        }

        public void OnDisable()
        {
            _attackEvent.Unsubscribe(OnAttack);
        }
        
        private void OnAttack()
        {
            if (!_attackCondition.Value) return;
            
            IAtomicAction<int> takeDamageEvent = _target.Value.GetAction<int>(ObjectAPI.TakeDamageAction);
            
            takeDamageEvent?.Invoke(_damage.Value);
        }
    }
}