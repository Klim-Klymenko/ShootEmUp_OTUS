using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(SwitchStateComponent))]
    public sealed class LevelBackground : MonoBehaviour, IGameFixedUpdateListener,
        IGameStartListener, IGameFinishListener, IGameResumeListener, IGamePauseListener
    {
        [SerializeField] private float _endPositionY;
        [SerializeField] private float _movingSpeedY;

        [SerializeField] private Vector3 _startingPosition;

        [SerializeField] private Transform _transform;
        [SerializeField] private SwitchStateComponent _switchComponent;
        
        public bool IsOnlyUnityMethods { get; } = false;

        private void OnValidate()
        {
            _transform = transform;
            _switchComponent = GetComponent<SwitchStateComponent>();
        }
        
        void IGameFixedUpdateListener.OnFixedUpdate()
        {
            if (_transform.position.y <= _endPositionY)
                _transform.position = _startingPosition;

            _transform.position -= Vector3.up * _movingSpeedY * Time.fixedDeltaTime;
        }

        void IGameStartListener.OnStart() => _switchComponent.TurnOn(this);
        
        void IGameFinishListener.OnFinish() => _switchComponent.TurnOff(this);
        
        void IGameResumeListener.OnResume() => _switchComponent.TurnOn(this);

        void IGamePauseListener.OnPause() => _switchComponent.TurnOff(this);
    }
}