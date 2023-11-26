using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(SwitchStateComponent))]
    [RequireComponent(typeof(MoveComponent))]
    public sealed class EnemyMoveAgent : MonoBehaviour, IGameFixedUpdateListener,
        IGameStartListener, IGameFinishListener, IGameResumeListener, IGamePauseListener
    {
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private SwitchStateComponent _switchComponent;

        private Transform _transform;
        
        private Vector2 _destination;

        public Vector2 Destination
        {
            set
            {
                IsReached = false;
                _destination = value;
            }
        }

        public bool IsReached { get; private set; }

        public bool IsOnlyUnityMethods { get; } = false;

        private void OnValidate()
        {
            _moveComponent = GetComponent<MoveComponent>();
            _switchComponent = GetComponent<SwitchStateComponent>();
        } 

        private void Awake() => _transform = transform;

        void IGameFixedUpdateListener.OnFixedUpdate()
        {
            if (IsReached) 
                return;
            
            Vector2 vectorToDestination = _destination - (Vector2) _transform.position;
            float vectorLength = vectorToDestination.magnitude;
            
            if (vectorLength <= 0.25f)
            {
                IsReached = true;
                return;
            }

            Vector2 normalizedVector = vectorToDestination / vectorLength;
            Vector2 displacement = normalizedVector * Time.fixedDeltaTime;
            _moveComponent.Move(displacement);
        }

        public void OnStart() => _switchComponent.TurnOn(this);

        void IGameFinishListener.OnFinish() => _switchComponent.TurnOff(this);

        void IGameResumeListener.OnResume() =>_switchComponent.TurnOn(this);

        void IGamePauseListener.OnPause() => _switchComponent.TurnOff(this);
    }
}