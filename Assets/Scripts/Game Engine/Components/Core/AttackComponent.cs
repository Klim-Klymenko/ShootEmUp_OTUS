using System;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class AttackComponent
    {
        [SerializeField]
        private AtomicValue<int> _damage;
        
        private AttackMechanics _attackMechanics;

        public void Compose(IAtomicObservable<AtomicObject> attackObservable, IAtomicValue<bool> attackCondition)
        {
            _attackMechanics = new AttackMechanics(attackObservable, attackCondition, _damage);
        }

        public void OnEnable()
        {
            _attackMechanics.OnEnable();
        }

        public void OnDisable()
        {
            _attackMechanics.OnDisable();
        }
    }
}