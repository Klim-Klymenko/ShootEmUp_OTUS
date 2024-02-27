using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;

namespace GameEngine
{
    public sealed class AttackMechanics
    {
        private readonly IAtomicObservable _attackObservable;
        private readonly IAtomicValue<bool> _attackCondition;
        private readonly IAtomicValue<int> _damage;
        private readonly IAtomicValue<AtomicObject> _target;

        public AttackMechanics(IAtomicObservable attackObservable, IAtomicValue<bool> attackCondition, IAtomicValue<int> damage,
            IAtomicValue<AtomicObject> target)
        {
            _attackObservable = attackObservable;
            _attackCondition = attackCondition;
            _damage = damage;
            _target = target;
        }

        public void OnEnable()
        {
            _attackObservable.Subscribe(OnAttack);
        }

        public void OnDisable()
        {
            _attackObservable.Unsubscribe(OnAttack);
        }
        
        private void OnAttack()
        {
            if (!_attackCondition.Value) return;
            
            IAtomicAction<int> takeDamageAction = _target.Value.GetAction<int>(LiveableAPI.TakeDamageAction);
            
            takeDamageAction?.Invoke(_damage.Value);
        }
    }
}