using System;
using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class RotationComponent : IDisposable
    {
        [SerializeField]
        private AtomicValue<float> _rotationSpeed;
        
        private readonly AtomicVariable<Vector3> _rotationDirection = new();
        
        private AndExpression _rotationCondition = new();
        
        private CalculateDirectionMechanics _calculateDirectionMechanics;
        private RotateMechanics _rotateMechanics;

        public void Compose(Transform transform, IAtomicValue<bool> aliveCondition, IAtomicValue<Vector3> positionToRotate)
        {
            _rotationCondition.Append(aliveCondition);
            _rotationCondition.Append(new AtomicFunction<bool>(() => _rotationDirection.Value != Vector3.zero));
            
            _calculateDirectionMechanics = new CalculateDirectionMechanics(_rotationDirection, positionToRotate, transform);
            _rotateMechanics = new RotateMechanics(_rotationDirection, _rotationSpeed, _rotationCondition, transform);
        }

        public void Update()
        {
            _calculateDirectionMechanics.Update();
            _rotateMechanics.Update();
        }

        public void Dispose()
        {
            _rotationDirection?.Dispose();
        }
    }
}