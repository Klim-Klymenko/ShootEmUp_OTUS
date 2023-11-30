using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(SwitchStateComponent))]
    [RequireComponent(typeof(EnemySpawner))]
    [RequireComponent(typeof(EnemySpawnTimer))]
    public sealed class EnemySpawnController : MonoBehaviour, IGameUpdateListener,
        IGameStartListener, IGameFinishListener, IGameResumeListener, IGamePauseListener
    {
        [SerializeField] private GameManager _gameManager;
        
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private EnemySpawnTimer _spawnTimer;

        [SerializeField] private SwitchStateComponent _switchComponent;

        public bool IsOnlyUnityMethods { get; } = false;
        
        private void OnValidate()
        {
            _enemySpawner = GetComponent<EnemySpawner>();
            _spawnTimer = GetComponent<EnemySpawnTimer>();

            _switchComponent = GetComponent<SwitchStateComponent>();
        }

        private void Enable() => _spawnTimer.OnTimeToSpawn += SpawnEnemy;

        private void Disable() => _spawnTimer.OnTimeToSpawn -= SpawnEnemy;

        void IGameUpdateListener.OnUpdate() =>
            _spawnTimer.TimerCountdown(_enemySpawner.ReservationAmount, _gameManager.CurrentGameState);

        private void SpawnEnemy() => _enemySpawner.SpawnEnemy();

        void IGameStartListener.OnStart()
        {
            _switchComponent.TurnOn(this);
            
            Enable();
        }

        void IGameFinishListener.OnFinish()
        {
            _switchComponent.TurnOff(this);
            
            Disable();
        }

        void IGameResumeListener.OnResume()
        {
            _switchComponent.TurnOn(this);
            
            Enable();
        }

        void IGamePauseListener.OnPause()
        {
            _switchComponent.TurnOff(this);
            
            Disable();
        } 
    }
}