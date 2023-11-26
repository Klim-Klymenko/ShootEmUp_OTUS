using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(EnemyReferenceComponent))]
    [RequireComponent(typeof(HitPointsComponent))]
    public sealed class EnemyDeathObserver : MonoBehaviour, IGameStartListener,
        IGameFinishListener, IGameResumeListener, IGamePauseListener
    {
        private EnemyManager EnemyManager => _referenceComponent.EnemyManager;
        
        [SerializeField] private HitPointsComponent _hitPointsComponent;

        [SerializeField] private EnemyReferenceComponent _referenceComponent;

        public bool IsOnlyUnityMethods { get; } = false;
        
        private void OnValidate()
        {
            _referenceComponent = GetComponent<EnemyReferenceComponent>();
            _hitPointsComponent = GetComponent<HitPointsComponent>();
        }

        private void Enable() => _hitPointsComponent.OnDeath += UnspawnEnemy;

        private void Disable() => _hitPointsComponent.OnDeath -= UnspawnEnemy;

        private void UnspawnEnemy() =>
            EnemyManager.UnspawnEnemy(_referenceComponent);

        public void OnStart() => Enable();

        void IGameFinishListener.OnFinish() => Disable();
        
        void IGameResumeListener.OnResume() => Enable();

        void IGamePauseListener.OnPause() => Disable();
    }
}