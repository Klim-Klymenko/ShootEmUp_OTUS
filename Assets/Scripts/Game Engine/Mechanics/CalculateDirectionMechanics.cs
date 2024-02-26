using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    public sealed class CalculateDirectionMechanics
    {
        private readonly IAtomicVariable<Vector3> _direction;
        private readonly IAtomicValue<Vector3> _targetPosition;
        private readonly Transform _transform;

        public CalculateDirectionMechanics(IAtomicVariable<Vector3> direction, IAtomicValue<Vector3> targetPosition, Transform transform)
        {
            _direction = direction;
            _targetPosition = targetPosition;
            _transform = transform;
        }

        public void Update()
        {
            Vector3 direction = (_targetPosition.Value - _transform.position).normalized;
            direction.y = 0;

            _direction.Value = direction;
        }
    }
}