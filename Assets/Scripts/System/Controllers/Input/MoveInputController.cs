using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using GameCycle;
using GameEngine;
using JetBrains.Annotations;
using UnityEngine;

namespace System
{
    [UsedImplicitly]
    internal sealed class MoveInputController : IStartGameListener, IUpdateGameListener
    {
        private const string HorizontalAxis = "Horizontal";
        private const string VerticalAxis = "Vertical";

        private IAtomicVariable<Vector3> _moveDirection;
        
        private readonly IAtomicObject _movable;

        internal MoveInputController(IAtomicObject movable)
        {
            _movable = movable;
        }

        void IStartGameListener.OnStart()
        {
            _moveDirection = _movable.GetVariable<Vector3>(MovableAPI.MoveDirection);
        }

        void IUpdateGameListener.OnUpdate()
        {
            float horizontal = Input.GetAxisRaw(HorizontalAxis);
            float vertical = Input.GetAxisRaw(VerticalAxis);
            
            _moveDirection.Value = new Vector3(horizontal, 0, vertical).normalized;
        }
    }
}