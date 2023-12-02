using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBackground : MonoBehaviour, IGameFixedUpdateListener
    {
        [SerializeField] private float _endPositionY;
        [SerializeField] private float _movingSpeedY;

        [SerializeField] private Vector3 _startingPosition;

        [SerializeField] private Transform _transform;

        private void OnValidate() => _transform = transform;

        void IGameFixedUpdateListener.OnFixedUpdate()
        {
            if (_transform.position.y <= _endPositionY)
                _transform.position = _startingPosition;

            _transform.position -= Vector3.up * _movingSpeedY * Time.fixedDeltaTime;
        }
    }
}