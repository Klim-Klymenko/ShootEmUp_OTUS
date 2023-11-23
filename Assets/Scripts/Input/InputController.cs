using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(SwitchStateComponent))]
    public sealed class InputController : MonoBehaviour, IGameUpdateListener, IGameFixedUpdateListener,
        IGameStartListener, IGameFinishListener, IGameResumeListener, IGamePauseListener
    {
        [SerializeField] private MoveComponent _characterMoveComponent;
        [SerializeField] private CharacterBulletManager bulletManager;

        [SerializeField] private SwitchStateComponent _switchComponent;
        
        private float _horizontal;

        public bool IsOnlyUnityMethods { get; } = false;


        private void OnValidate() => _switchComponent = GetComponent<SwitchStateComponent>();
        
        void IGameUpdateListener.OnUpdate()
        {
            _horizontal = Input.GetAxis("Horizontal");
            
            if (Input.GetKeyDown(KeyCode.Space))
                bulletManager.RunBullet();
        }
        
        void IGameFixedUpdateListener.OnFixedUpdate()
        {
            _characterMoveComponent.Move(new Vector2(_horizontal, 0) * Time.fixedDeltaTime);
        }

        public void OnStart() => _switchComponent.TurnOn(this);
        
        public void OnFinish() => _switchComponent.TurnOff(this);
        
        public void OnResume() => _switchComponent.TurnOn(this);
        
        public void OnPause() => _switchComponent.TurnOff(this);
    }
}