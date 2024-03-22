using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;

namespace GameEngine
{
    public sealed class AttackMechanics
    {
        private readonly IAtomicObservable<AtomicObject> _attackObservable;
        private readonly IAtomicValue<bool> _attackCondition;
        private readonly IAtomicValue<int> _damage;

        public AttackMechanics(IAtomicObservable<AtomicObject> attackObservable, IAtomicValue<bool> attackCondition, IAtomicValue<int> damage)
        {
            _attackObservable = attackObservable;
            _attackCondition = attackCondition;
            _damage = damage;
        }

        public void OnEnable()
        {
            _attackObservable.Subscribe(OnAttack);
        }

        public void OnDisable()
        {
            _attackObservable.Unsubscribe(OnAttack);
        }
        
        private void OnAttack(AtomicObject target)
        {
            if (!_attackCondition.Value) return;
            
            IAtomicAction<int> takeDamageAction = target.GetAction<int>(LiveableAPI.TakeDamageAction);
            takeDamageAction?.Invoke(_damage.Value);
        }
    }
}