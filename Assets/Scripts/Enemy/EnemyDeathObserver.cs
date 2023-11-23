using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(SwitchStateComponent))]
    public sealed class EnemyDeathObserver : MonoBehaviour, IGameStartListener,
        IGameFinishListener, IGameResumeListener, IGamePauseListener
    {
        private EnemyManager EnemyManager => _referenceComponent.EnemyManager;
        
        [SerializeField] private HitPointsComponent _hitPointsComponent;

        [SerializeField] private EnemyReferenceComponent _referenceComponent;

        [SerializeField] private SwitchStateComponent _switchComponent;

        public bool IsOnlyUnityMethods { get; } = false;
        
        private void OnValidate()
        {
            _referenceComponent = GetComponent<EnemyReferenceComponent>();
            _hitPointsComponent = GetComponent<HitPointsComponent>();

            _switchComponent = GetComponent<SwitchStateComponent>();
        }

        private void Enable() => _hitPointsComponent.OnDeath += UnspawnEnemy;

        private void Disable() => _hitPointsComponent.OnDeath -= UnspawnEnemy;

        private void UnspawnEnemy() =>
            EnemyManager.UnspawnEnemy(_referenceComponent);

        public void OnStart() => Enable();

        public void OnFinish() => Disable();
        
        public void OnResume() => Enable();

        public void OnPause() => Disable();
    }
}