using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using GameCycle;
using GameEngine;
using JetBrains.Annotations;
using UnityEngine;

namespace System
{
    [UsedImplicitly]
    internal sealed class RotateInputController : IStartGameListener, IUpdateGameListener
    {
        private IAtomicVariable<Vector3> _rotateDirection;
        
        private readonly Camera _camera;
        private readonly Transform _transform;
        private readonly IAtomicObject _rotatable;

        internal RotateInputController(Camera camera, Transform transform, IAtomicObject rotatable)
        {
            _camera = camera;
            _transform = transform;
            _rotatable = rotatable;
        }

        void IStartGameListener.OnStart()
        {
            _rotateDirection = _rotatable.GetVariable<Vector3>(RotatableAPI.RotateDirection);
        }
        
        void IUpdateGameListener.OnUpdate()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out RaycastHit hit)) return;
            
            Vector3 targetPosition = hit.point;
            
            Vector3 direction = (targetPosition - _transform.position).normalized;
            direction.y = 0;

            _rotateDirection.Value = direction;
        }
    }
}