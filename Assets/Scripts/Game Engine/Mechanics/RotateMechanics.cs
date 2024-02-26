using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    public sealed class RotateMechanics
    {
        private readonly IAtomicValue<Vector3> _direction;
        private readonly IAtomicValue<float> _rotationSpeed;
        private readonly IAtomicValue<bool> _rotationCondition;
        private readonly Transform _transform;

        public RotateMechanics(IAtomicValue<Vector3> direction, IAtomicValue<float> rotationSpeed, IAtomicValue<bool> rotationCondition, Transform transform)
        {
            _direction = direction;
            _rotationSpeed = rotationSpeed;
            _rotationCondition = rotationCondition;
            _transform = transform;
        }

        public void Update()
        {
            if (!_rotationCondition.Value) return;
            
            Quaternion rotation = Quaternion.LookRotation(_direction.Value);
            Quaternion smoothRotation = Quaternion.Slerp(_transform.rotation, rotation, _rotationSpeed.Value * Time.deltaTime);
            
            _transform.rotation = smoothRotation;
        }
    }
}