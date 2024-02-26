using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    public sealed class MoveInputController
    {
        private const string HorizontalAxis = "Horizontal";
        private const string VerticalAxis = "Vertical";
        
        private readonly IAtomicVariable<Vector3> _moveDirection;

        public MoveInputController(IAtomicVariable<Vector3> moveDirection)
        {
            _moveDirection = moveDirection;
        }

        public void Update()
        {
            float horizontal = Input.GetAxisRaw(HorizontalAxis);
            float vertical = Input.GetAxisRaw(VerticalAxis);
            
            _moveDirection.Value = new Vector3(horizontal, 0, vertical).normalized;
        }
    }
}