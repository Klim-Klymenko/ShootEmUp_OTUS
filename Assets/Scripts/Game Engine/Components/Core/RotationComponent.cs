using System;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class RotationComponent : IDisposable
    {
        [SerializeField]
        private AtomicValue<float> _rotationSpeed;
        
        [Get(RotatableAPI.RotateDirection)]
        private readonly AtomicVariable<Vector3> _rotationDirection = new();
        
        private readonly AndExpression _rotationCondition = new();
        
        private RotateMechanics _rotateMechanics;

        public IAtomicExpression<bool> RotationCondition => _rotationCondition;

        public void Compose(Transform transform)
        {
            _rotationCondition.Append(new AtomicFunction<bool>(() => _rotationDirection.Value != Vector3.zero));
            
            _rotateMechanics = new RotateMechanics(_rotationDirection, _rotationSpeed, _rotationCondition, transform);
        }

        public void Update()
        {
            _rotateMechanics.Update();
        }

        public void Dispose()
        {
            _rotationDirection?.Dispose();
        }
    }
}