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
        private AtomicValue<float> _distanceToTarget;
        
        [SerializeField]
        private MoveComponent _moveComponent;
        
        private readonly AtomicVariable<Vector3> _direction = new();
        
        [SerializeField]
        [HideInInspector]
        private AtomicFunction<Vector3> _targetPosition;
        
        private CalculateDirectionMechanics _calculateDirectionMechanics;
        
        public override void Compose()
        {
            base.Compose();

            IAtomicValue<bool> aliveCondition = _target.GetValue<bool>(LiveableAPI.AliveCondition);
            
            _targetPosition.Compose(() => _targetTransform.position - Vector3.forward * _distanceToTarget.Value);
            _moveComponent.Compose(_transform, aliveCondition, _direction);

            _calculateDirectionMechanics = new CalculateDirectionMechanics(_direction, _targetPosition, _transform);
        }

        void IStartGameListener.OnStart()
        {
            Compose();
        }

        void ILateUpdateGameListener.OnLateUpdate()
        {
            _calculateDirectionMechanics.Update();
            _moveComponent.Update();
        }
    }
}