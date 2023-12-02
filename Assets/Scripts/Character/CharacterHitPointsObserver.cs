using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(HitPointsComponent))]
    public sealed class CharacterHitPointsObserver : MonoBehaviour, IGameStartListener,
        IGameFinishListener, IGameResumeListener, IGamePauseListener
    {
        [SerializeField] private GameManager _gameManager;

        [SerializeField] private HitPointsComponent _hitPointsComponent;

        private void OnValidate() => _hitPointsComponent = GetComponent<HitPointsComponent>();

        private void Enable() => _hitPointsComponent.OnDeath += CharacterDeath;

        private void Disable() =>_hitPointsComponent.OnDeath -= CharacterDeath;

        private void CharacterDeath() => _gameManager.OnFinish();
        
        void IGameStartListener.OnStart() => Enable();

        void IGameFinishListener.OnFinish() => Disable();

        void IGameResumeListener.OnResume() => Enable();
        
        void IGamePauseListener.OnPause() => Disable();
        
    }  
}