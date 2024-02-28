using System;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    [Is(ObjectTypes.Attacker)]
    public sealed class AttackComponent : IDisposable
    {
        [SerializeField]
        [Get(AttackerAPI.AttackTargetTransform)]
        private AtomicVariable<Transform> _targetTransform;
        
        [SerializeField]
        private AtomicValue<float> _attackRange;
        
        [SerializeField]
        private AtomicValue<int> _damage;
        
        [SerializeField]
        private AtomicValue<float> _attackInterval;
        
        private readonly AtomicEvent _attackRequestEvent = new();
        
        [Get(AttackerAPI.AttackAction)]
        private readonly AtomicEvent<AtomicObject> _attackEvent = new();
        
        private readonly AtomicEvent _attackEventNonArgs = new();
        
        [SerializeField]
        [HideInInspector]
        private AndExpression _attackCondition;

        [SerializeField]
        [HideInInspector]
        private IsInAttackRangeFunction _isInAttackRange;

        private CooldownMechanics _cooldownMechanics;
        private AttackMechanics _attackMechanics;

        public IAtomicObservable AttackRequestEvent => _attackRequestEvent;
        public IAtomicObservable AttackEvent => _attackEventNonArgs;
        
        public void Compose(IAtomicValue<bool> aliveCondition, Transform transform)
        {
            _isInAttackRange.Compose(_attackRange, _targetTransform, transform);
            
            _attackCondition.Append(aliveCondition);
            _attackCondition.Append(new AtomicFunction<bool>(() => _targetTransform.Value != null));
            _attackCondition.Append(_isInAttackRange);

            _attackEvent.Subscribe(_ => _attackEventNonArgs?.Invoke());
            
            _cooldownMechanics = new CooldownMechanics(_attackRequestEvent, _attackInterval, _attackCondition);
            _attackMechanics = new AttackMechanics(_attackEvent, _attackCondition, _damage);
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
            _targetTransform?.Dispose();
            _attackRequestEvent?.Dispose();
            _attackEvent?.Dispose();
            _attackEventNonArgs?.Dispose();
        }
    }
}