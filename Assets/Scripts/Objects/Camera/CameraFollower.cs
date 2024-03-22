using GameCycle;
using UnityEngine;

namespace Objects
{
    internal sealed class CameraFollower : MonoBehaviour, ILateUpdateGameListener
    {
        [SerializeField]
        private Transform _transform;

        [SerializeField]
        private Transform _targetTransform;
        
        [SerializeField]
        private Vector3 _offsetFromTarget;
        
        private void OnValidate()
        {
            _transform = transform;
        }

        void ILateUpdateGameListener.OnLateUpdate()
        {
            _transform.position = _targetTransform.position + _offsetFromTarget;
        }
    }
}