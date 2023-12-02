using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputMoveController : MonoBehaviour, IGameUpdateListener, IGameFixedUpdateListener
    {
        [SerializeField] private MoveComponent _characterMoveComponent;
        
        private float _horizontal;
        
        void IGameUpdateListener.OnUpdate() => _horizontal = Input.GetAxis("Horizontal");

        void IGameFixedUpdateListener.OnFixedUpdate() => 
            _characterMoveComponent.Move(new Vector2(_horizontal, 0) * Time.fixedDeltaTime);
    }
}