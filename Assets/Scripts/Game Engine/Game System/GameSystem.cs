using System;
using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using UnityEngine;
using GameCycle;

namespace GameEngine
{
    public sealed class GameSystem : MonoBehaviour, IStartGameListener, IUpdateGameListener
    {
        [SerializeField]
        private AtomicObject _character;
        
        private MoveInputController _moveInputController;
        private ShootInputController _shootInputController;
        
        void IStartGameListener.OnStart()
        {
            if (_character.Is(ObjectTypes.Movable))
            {
                IAtomicVariable<Vector3> moveDirection = _character.GetVariable<Vector3>(MovableAPI.MoveDirection);
                _moveInputController = new MoveInputController(moveDirection);
            }
            
            if (_character.Is(ObjectTypes.Striker))
            {
                IAtomicAction shootAction = _character.GetAction(ShooterAPI.ShootAction);
                IAtomicValue<float> shootingInterval = _character.GetValue<float>(ShooterAPI.ShootingInterval);
                _shootInputController = new ShootInputController(shootAction, shootingInterval);
            }
        }
            
        void IUpdateGameListener.OnUpdate()
        {
            _moveInputController.Update();
            _shootInputController.Update();
        }
    }
}