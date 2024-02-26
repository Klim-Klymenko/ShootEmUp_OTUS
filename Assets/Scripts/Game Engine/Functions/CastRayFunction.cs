using System;
using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class CastRayFunction : IAtomicFunction<Vector3>
    {
        private IAtomicValue<Vector3> _castPosition;
        private Camera _camera;
        private Transform _transform;

        public void Compose(IAtomicValue<Vector3> castPosition, Camera camera, Transform transform)
        {
            _castPosition = castPosition;
            _camera = camera;
            _transform = transform;
        }

        Vector3 IAtomicFunction<Vector3>.Invoke()
        {
            Ray ray = _camera.ScreenPointToRay(_castPosition.Value);

            return Physics.Raycast(ray, out RaycastHit hitInfo) ? hitInfo.point : default;
        }
    }
}