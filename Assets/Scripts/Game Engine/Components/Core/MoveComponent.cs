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
        private AtomicValue<float> _moveSpeed;
        
        [Get(MovableAPI.MoveDirection)]
        private AtomicVariable<Vector3> _moveDirection = new();
        
        private readonly AndExpression _moveCondition = new();
        
        private MoveMechanics _moveMechanics;
        
        [Get(MovableAPI.MoveCondition)]
        public IAtomicExpression<bool> MoveCondition => _moveCondition;
        
        public void Compose(Transform transform, AtomicVariable<Vector3> direction = null)
        {
            if (direction != null)
                _moveDirection = direction;
            
            _moveCondition.Append(new AtomicFunction<bool>(() => _moveDirection.Value != Vector3.zero));
            
            _moveMechanics = new MoveMechanics(_moveDirection, _moveSpeed, _moveCondition, transform);
        }
        
        public void Update()
        {
            _moveMechanics.Update();
        }

        public void Dispose()
        {
            _moveDirection?.Dispose();
        }
    }
}