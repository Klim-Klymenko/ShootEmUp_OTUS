using System;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    [Is(ObjectTypes.Movable)]
    public sealed class MoveComponent : IDisposable
    {
        [SerializeField]
        private Transform _transform;

        [SerializeField] 
        private Camera _camera;
        
        [SerializeField] 
        private AtomicValue<float> _moveSpeed;

        [SerializeField]
        private AtomicValue<float> _rotationSpeed;
        
        [Get(ObjectAPI.MoveDirection)]
        private readonly AtomicVariable<Vector3> _moveDirection = new();

        private readonly AtomicVariable<Vector3> _rotationDirection = new();
        
        [SerializeField]
        private AndExpression _moveCondition;
        
        [SerializeField]
        private AndExpression _rotationCondition;
        
        [SerializeField]
        private CastRayFunction _castRayFunction;
        
        private MoveMechanics _moveMechanics;
        private CalculateDirectionMechanics _calculateDirectionMechanics;
        private RotateMechanics _rotateMechanics;
        
        [Get(ObjectAPI.IsMoving)]
        public IAtomicValue<bool> MoveCondition => _moveCondition;
        
        public void Compose(IAtomicValue<bool> isAlive)
        {
            _moveCondition.Append(isAlive);
            _moveCondition.Append(new AtomicFunction<bool>(() => _moveDirection.Value != Vector3.zero));
            
            _rotationCondition.Append(isAlive);
            _rotationCondition.Append(new AtomicFunction<bool>(() => _rotationDirection.Value != Vector3.zero));
            
            AtomicProperty<Vector3> mousePosition = new(() => Input.mousePosition, null);
            
            _castRayFunction.Compose(mousePosition, _camera, _transform);
            
            _moveMechanics = new MoveMechanics(_moveDirection, _moveSpeed, _moveCondition, _transform);
            _calculateDirectionMechanics = new CalculateDirectionMechanics(_rotationDirection, _castRayFunction, _transform);
            _rotateMechanics = new RotateMechanics(_rotationDirection, _rotationSpeed, _rotationCondition, _transform);
        }
        
        public void Update()
        {
            _moveMechanics.Update();
            _calculateDirectionMechanics.Update();
            _rotateMechanics.Update();
        }

        public void Dispose()
        {
            _moveDirection?.Dispose();
        }
    }
}