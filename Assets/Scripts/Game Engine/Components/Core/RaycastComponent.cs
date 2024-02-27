using System;
using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class RaycastComponent
    {
        [SerializeField] 
        private Camera _camera;
        
        [SerializeField]
        private CastRayFunction _castedPosition;

        public IAtomicValue<Vector3> CastedPosition => _castedPosition;
        
        public void Compose(IAtomicValue<Vector3> positionToCast)
        {
            _castedPosition.Compose(positionToCast, _camera);
        }
    }
}