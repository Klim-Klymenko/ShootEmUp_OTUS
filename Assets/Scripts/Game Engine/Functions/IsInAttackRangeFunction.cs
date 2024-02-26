using System;
using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class IsInAttackRangeFunction : IAtomicFunction<bool>
    {
        private IAtomicValue<float> _attackRange;
        private IAtomicValue<Transform> _targetTransform;
        private Transform _transform;
        
        public void Compose(IAtomicValue<float> attackRange, IAtomicValue<Transform> targetTransform, Transform transform)
        {
            _attackRange = attackRange;
            _targetTransform = targetTransform;
            _transform = transform;
        }

        bool IAtomicFunction<bool>.Invoke()
        {
            float sqrAttackRange = _attackRange.Value * _attackRange.Value;

            Vector3 vectorToTarget = _targetTransform.Value.position - _transform.position;
            float sqrDistanceToTarget = vectorToTarget.sqrMagnitude;

            return sqrDistanceToTarget <= sqrAttackRange;
        }
    }
}