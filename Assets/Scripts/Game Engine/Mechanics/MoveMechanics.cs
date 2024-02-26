using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    public sealed class MoveMechanics
    {
        private readonly IAtomicValue<Vector3> _direction;
        private readonly IAtomicValue<float> _speed;
        private readonly IAtomicValue<bool> _moveCondition;
        private readonly Transform _transform;

        public MoveMechanics(IAtomicValue<Vector3> direction, IAtomicValue<float> speed, IAtomicValue<bool> moveCondition, Transform transform)
        {
            _direction = direction;
            _speed = speed;
            _moveCondition = moveCondition;
            _transform = transform;
        }

        public void Update()
        {
            if (!_moveCondition.Value) return;
            
            _transform.position += _direction.Value * (_speed.Value * Time.deltaTime);
        }
    }
}