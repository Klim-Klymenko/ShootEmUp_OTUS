using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using GameCycle;
using GameEngine;
using UnityEngine;

namespace Objects
{
    public sealed class Camera : AtomicObject, IStartGameListener, ILateUpdateGameListener
    {
        [SerializeField]
        private Transform _transform;

        [SerializeField]
        private Transform _targetTransform;

        [SerializeField]
        private AtomicObject _target;
        
        [SerializeField] 
        private AtomicValue<float> _followingSpeed;
        
        [SerializeField]
        private AtomicValue<float> _distanceToTarget;
        
        private readonly AtomicVariable<Vector3> _direction = new();
        
        [SerializeField]
        private AtomicFunction<Vector3> _targetPosition;
        
        private CalculateDirectionMechanics _calculateDirectionMechanics;
        private MoveMechanics _moveMechanics;
        
        public override void Compose()
        {
            base.Compose();

            IAtomicValue<bool> isMoving = _target.GetValue<bool>(ObjectAPI.IsMoving);
            
            _targetPosition.Compose(() => _targetTransform.position - Vector3.forward * _distanceToTarget.Value);
            
            _calculateDirectionMechanics = new CalculateDirectionMechanics(_direction, _targetPosition, _transform);
            _moveMechanics = new MoveMechanics(_direction, _followingSpeed, isMoving, _transform);
        }

        void IStartGameListener.OnStart()
        {
            Compose();
        }

        void ILateUpdateGameListener.OnLateUpdate()
        {
            _calculateDirectionMechanics.Update();
            _moveMechanics.Update();
        }
    }
}