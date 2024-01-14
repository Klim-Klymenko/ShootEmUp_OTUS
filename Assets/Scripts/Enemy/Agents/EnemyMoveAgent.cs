using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : IGameFixedUpdateListener
    {
        private MoveComponent _moveComponent;
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

        [Inject]
        public void Construct(EnemyService enemyService)
        {
            _moveComponent = enemyService.MoveComponent;
            _transform = enemyService.Transform;
        }
        
        void IGameFixedUpdateListener.OnFixedUpdate()
        {
            if (IsReached) return;
            
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
    }
}