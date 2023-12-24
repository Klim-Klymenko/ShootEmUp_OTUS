using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputMoveController : IGameUpdateListener, IGameFixedUpdateListener
    {
        private MoveComponent _characterMoveComponent;
        private float _horizontal;
        
        [Inject]
        private void Construct(MoveComponent characterMoveComponent)
        {
            _characterMoveComponent = characterMoveComponent;
        }

        void IGameUpdateListener.OnUpdate() => _horizontal = Input.GetAxis("Horizontal");

        void IGameFixedUpdateListener.OnFixedUpdate() => 
            _characterMoveComponent.Move(new Vector2(_horizontal, 0) * Time.fixedDeltaTime);
    }
}