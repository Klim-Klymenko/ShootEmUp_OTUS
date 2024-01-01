using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputMoveController : IGameUpdateListener, IGameFixedUpdateListener
    {
        private const string HorizontalAxis = "Horizontal";
        private float _horizontal;
        
        private MoveComponent _characterMoveComponent;

        [Inject]
        private void Construct(CharacterService characterService)
        {
            _characterMoveComponent = characterService.MoveComponent;
        }

        void IGameUpdateListener.OnUpdate() => _horizontal = Input.GetAxis(HorizontalAxis);

        void IGameFixedUpdateListener.OnFixedUpdate() => 
            _characterMoveComponent.Move(new Vector2(_horizontal, 0) * Time.fixedDeltaTime);
    }
}