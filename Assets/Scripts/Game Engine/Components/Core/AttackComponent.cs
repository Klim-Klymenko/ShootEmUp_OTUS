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
        [Get(AttackerAPI.AttackTarget)]
        private AtomicVariable<AtomicObject> _target;
        
        [SerializeField]
        [Get(AttackerAPI.AttackTargetTransform)]
        private AtomicVariable<Transform> _targetTransform;
        
        [SerializeField]
        private AtomicValue<float> _attackRange;
        
        [SerializeField]
        private AtomicValue<int> _damage;
        
        [SerializeField]
        private AtomicValue<float> _attackInterval;

        [SerializeField]
        [HideInInspector]
        private AtomicEvent _attackRequestEvent;

        [SerializeField]
        [HideInInspector]
        private AtomicEvent _attackEvent;

        [SerializeField]
        [HideInInspector]
        private AndExpression _attackCondition;

        [SerializeField]
        [HideInInspector]
        private IsInAttackRangeFunction _isInAttackRange;

        private CooldownMechanics _cooldownMechanics;
        private AttackMechanics _attackMechanics;

        public IAtomicObservable AttackRequestEvent => _attackRequestEvent;
        
        [Get(AttackerAPI.AttackAction)]
        public IAtomicObservable AttackEvent => _attackEvent;
        
        public void Compose(IAtomicValue<bool> aliveCondition, Transform transform)
        {
            _isInAttackRange.Compose(_attackRange, _targetTransform, transform);
            
            _attackCondition.Append(aliveCondition);
            _attackCondition.Append(new AtomicFunction<bool>(() => _target.Value != null));
            _attackCondition.Append(_isInAttackRange);

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