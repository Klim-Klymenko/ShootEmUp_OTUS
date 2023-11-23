using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(SwitchStateComponent))]
    public sealed class CharacterHitPointsObserver : MonoBehaviour, IGameStartListener,
        IGameFinishListener, IGameResumeListener, IGamePauseListener
    {
        [SerializeField] private GameManager _gameManager;

        [SerializeField] private HitPointsComponent _hitPointsComponent;

        [SerializeField] private SwitchStateComponent _switchComponent;
        public bool IsOnlyUnityMethods { get; } = false;
        
        private void OnValidate()
        {
            _hitPointsComponent = GetComponent<HitPointsComponent>();
            _switchComponent = GetComponent<SwitchStateComponent>();
        }

        private void Enable() => _hitPointsComponent.OnDeath += CharacterDeath;

        private void Disable() =>_hitPointsComponent.OnDeath -= CharacterDeath;

        private void CharacterDeath() => _gameManager.FinishGame();
        
        public void OnStart() => Enable();

        public void OnFinish() => Disable();

        public void OnResume() => Enable();
        
        public void OnPause() => Disable();
        
    }  
}