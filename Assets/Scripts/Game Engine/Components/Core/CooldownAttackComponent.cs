using System;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    [Is(ObjectTypes.Attacker)]
    public sealed class CooldownAttackComponent : IDisposable
    {
        [SerializeField]
        [Get(AttackerAPI.AttackTargetTransform)]
        private AtomicVariable<Transform> _targetTransform;
        
        [SerializeField]
        private AtomicValue<float> _attackRange;
        
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

        [SerializeField] 
        private CooldownComponent _cooldownComponent;
        
        [SerializeField]
        private AttackComponent _attackComponent;

        public IAtomicObservable AttackRequestEvent => _attackRequestEvent;
        public IAtomicObservable AttackEvent => _attackEventNonArgs;
        
        public void Compose(IAtomicValue<bool> aliveCondition, Transform transform)
        {
            _isInAttackRange.Compose(_attackRange, _targetTransform, transform);
            
            _attackCondition.Append(aliveCondition);
            _attackCondition.Append(new AtomicFunction<bool>(() => _targetTransform.Value != null));
            _attackCondition.Append(_isInAttackRange);

            _attackEvent.Subscribe(_ => _attackEventNonArgs?.Invoke());
            
            _cooldownComponent.Compose(_attackRequestEvent, _attackCondition);
            _attackComponent.Compose(_attackEvent, _attackCondition);
        }

        public void OnEnable()
        {
            _attackComponent.OnEnable();
        }
        
        public void Update()
        {
            _cooldownComponent.Update();
        }
        
        public void OnDisable()
        {
            _attackComponent.OnDisable();
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