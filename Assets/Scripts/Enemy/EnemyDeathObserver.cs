using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(EnemyReferenceComponent))]
    [RequireComponent(typeof(HitPointsComponent))]
    public sealed class EnemyDeathObserver : MonoBehaviour, IGameStartListener,
        IGameFinishListener, IGameResumeListener, IGamePauseListener
    {
        private EnemySpawner _enemySpawner;
        public EnemySpawner EnemySpawner
        {
            set => _enemySpawner = value;
        }

        [SerializeField] private HitPointsComponent _hitPointsComponent;

        [SerializeField] private EnemyReferenceComponent _referenceComponent;

        private void OnValidate()
        {
            _referenceComponent = GetComponent<EnemyReferenceComponent>();
            _hitPointsComponent = GetComponent<HitPointsComponent>();
        }

        private void Enable() => _hitPointsComponent.OnDeath += UnspawnEnemy;

        private void Disable() => _hitPointsComponent.OnDeath -= UnspawnEnemy;

        private void UnspawnEnemy() =>
            _enemySpawner.UnspawnEnemy(_referenceComponent);

        void IGameStartListener.OnStart() => Enable();

        void IGameFinishListener.OnFinish() => Disable();
        
        void IGameResumeListener.OnResume() => Enable();

        void IGamePauseListener.OnPause() => Disable();
    }
}