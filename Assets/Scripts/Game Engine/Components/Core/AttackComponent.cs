using System;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameEngine
{
    [Serializable]
    [Is(ObjectTypes.Attacker)]
    public sealed class AttackComponent : IDisposable
    {
        [SerializeField]
        [Get(ObjectAPI.AttackTarget)]
        private AtomicVariable<AtomicObject> _target;
        
        [SerializeField]
        [Get(ObjectAPI.AttackTargetTransform)]
        private AtomicVariable<Transform> _targetTransform;
        
        [SerializeField]
        private AtomicValue<float> _attackRange;
        
        [SerializeField]
        private AtomicValue<int> _damage;
        
        [SerializeField]
        private AtomicValue<float> _attackInterval;

        [SerializeField]
        private AtomicEvent _attackRequestEvent;

        [SerializeField]
        [Get(ObjectAPI.AttackAction)]
        private AtomicEvent _attackEvent;

        [SerializeField]
        private AndExpression _attackCondition;

        [SerializeField]
        private IsInAttackRangeFunction _isInAttackRangeFunction;

        private CooldownMechanics _cooldownMechanics;
        private AttackMechanics _attackMechanics;

        public IAtomicObservable AttackRequestEvent => _attackRequestEvent;
        public IAtomicObservable AttackEvent => _attackEvent;
        
        public void Compose(IAtomicValue<bool> isAlive, Transform transform)
        {
            _isInAttackRangeFunction.Compose(_attackRange, _targetTransform, transform);
            
            _attackCondition.Append(isAlive);
            _attackCondition.Append(new AtomicFunction<bool>(() => _target.Value != null));
            _attackCondition.Append(_isInAttackRangeFunction);

            _cooldownMechanics = new CooldownMechanics(_attackRequestEvent, _attackInterval, _attackCondition);
            _attackMechanics = new AttackMechanics(_attackEvent, _attackCondition, _damage, _target);
        }

        public void OnEnable()
        {
            _attackMechanics.OnEnable();
        }
        
        public void Update()
        {
            _cooldownMechanics.Update();
        }
        
        public void OnDisable()
        {
            _attackMechanics.OnDisable();
        }

        public void Dispose()
        {
            _target?.Dispose();
            _targetTransform?.Dispose();
            _attackRequestEvent?.Dispose();
        }
    }
}