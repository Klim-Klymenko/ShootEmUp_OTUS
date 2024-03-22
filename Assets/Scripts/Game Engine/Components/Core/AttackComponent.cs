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
        
        private readonly AndExpression _attackCondition = new();
        
        private AttackMechanics _attackMechanics;
        
        public IAtomicExpression<bool> AttackCondition => _attackCondition;

        public void Compose(IAtomicObservable<AtomicObject> attackObservable)
        {
            _attackMechanics = new AttackMechanics(attackObservable, _attackCondition, _damage);
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