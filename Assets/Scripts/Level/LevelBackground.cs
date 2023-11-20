using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBackground : MonoBehaviour
    {
        [SerializeField] private float _endPositionY;
        [SerializeField] private float _movingSpeedY;

        [SerializeField] private Vector3 _startingPosition;

        private Transform _transform;

        private void Awake() => _transform = transform;

        private void FixedUpdate()
        {
            if (_transform.position.y <= _endPositionY)
                _transform.position = _startingPosition;

            _transform.position -= Vector3.up * _movingSpeedY * Time.fixedDeltaTime;
        }
    }
}