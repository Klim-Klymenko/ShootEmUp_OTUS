using System;
using Atomic.Elements;

namespace GameEngine
{
    [Serializable]
    public sealed class ShootAction : IAtomicAction
    {
        private IAtomicVariable<int> _charges;
        private IAtomicAction _spawnBulletAction;
        private IAtomicAction _shootEvent;
        private IAtomicValue<bool> _shootCondition;

        public void Compose(IAtomicVariable<int> charges, IAtomicAction spawnBulletAction, IAtomicAction shootEvent, IAtomicValue<bool> shootCondition)
        {
            _charges = charges;
            _spawnBulletAction = spawnBulletAction;
            _shootEvent = shootEvent;
            _shootCondition = shootCondition;
        }

        void IAtomicAction.Invoke()
        {
            if (!_shootCondition.Value) return;
            
            _spawnBulletAction.Invoke();
            _shootEvent.Invoke();
            
            _charges.Value--;
        }
    }
}