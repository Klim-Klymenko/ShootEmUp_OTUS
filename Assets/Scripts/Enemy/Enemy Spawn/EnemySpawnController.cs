namespace ShootEmUp
{
    public sealed class EnemySpawnController : IGameUpdateListener,
        IGameStartListener, IGameFinishListener, IGameResumeListener, IGamePauseListener
    {
        private GameManager _gameManager;
        
        private EnemySpawner _enemySpawner;
        private readonly EnemySpawnTimer _spawnTimer = new();
        
        [Inject]
        public void Construct(GameManager gameManager, EnemySpawner enemySpawner)
        {
            _gameManager = gameManager;
            _enemySpawner = enemySpawner;
        }
        
        private void Enable() => _spawnTimer.OnTimeToSpawn += SpawnEnemy;

        private void Disable() => _spawnTimer.OnTimeToSpawn -= SpawnEnemy;

        void IGameUpdateListener.OnUpdate() =>
            _spawnTimer.TimerCountdown(_enemySpawner.ReservationAmount, _gameManager.CurrentGameState);

        private void SpawnEnemy() => _enemySpawner.SpawnEnemy();

        void IGameStartListener.OnStart() => Enable();
        
        void IGameFinishListener.OnFinish() => Disable();

        void IGameResumeListener.OnResume() => Enable();

        void IGamePauseListener.OnPause() => Disable();
    }
}