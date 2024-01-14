using UnityEngine;

namespace ShootEmUp
{
    [System.Serializable]
    public sealed class LevelBackground : IGameFixedUpdateListener
    {
        [SerializeField]
        private float _endPositionY;
        
        [SerializeField]
        private float _movingSpeedY;

        [SerializeField]
        private Vector3 _startingPosition;

        [SerializeField]
        private Transform _transform;

        void IGameFixedUpdateListener.OnFixedUpdate()
        {
            if (_transform.position.y <= _endPositionY)
                _transform.position = _startingPosition;

            _transform.position -= Vector3.up * _movingSpeedY * Time.fixedDeltaTime;
        }
    }
}