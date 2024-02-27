using System;
using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class CastRayFunction : IAtomicFunction<Vector3>
    {
        private IAtomicValue<Vector3> _positionToCast;
        private Camera _camera;

        public void Compose(IAtomicValue<Vector3> positionToCast, Camera camera)
        {
            _positionToCast = positionToCast;
            _camera = camera;
        }

        Vector3 IAtomicFunction<Vector3>.Invoke()
        {
            Ray ray = _camera.ScreenPointToRay(_positionToCast.Value);

            return Physics.Raycast(ray, out RaycastHit hitInfo) ? hitInfo.point : default;
        }
    }
}