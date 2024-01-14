namespace ShootEmUp
{
    public sealed class EnemyDeathObserver : IGameStartListener,
        IGameFinishListener, IGameResumeListener, IGamePauseListener
    {
        private EnemySpawner _enemySpawner;
        
        private readonly HitPointsComponent _hitPointsComponent;
        private readonly EnemyReferenceComponent _referenceComponent;

        public EnemyDeathObserver(EnemyService enemyService, EnemyReferenceComponent referenceComponent)
        {
            _hitPointsComponent = enemyService.HitPointsComponent;
            _referenceComponent = referenceComponent;
        }

        [Inject]
        public void Construct(EnemySpawner enemySpawner)
        {
            _enemySpawner = enemySpawner;
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