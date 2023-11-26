using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(SwitchStateComponent))]
    [RequireComponent(typeof(EnemyManager))]
    [RequireComponent(typeof(EnemySpawnTimer))]
    public sealed class EnemySpawnController : MonoBehaviour, IGameUpdateListener,
        IGameStartListener, IGameFinishListener, IGameResumeListener, IGamePauseListener
    {
        [SerializeField] private GameManager _gameManager;
        
        [SerializeField] private EnemyManager _enemyManager;
        [SerializeField] private EnemySpawnTimer _spawnTimer;

        [SerializeField] private SwitchStateComponent _switchComponent;

        public bool IsOnlyUnityMethods { get; } = false;
        
        private void OnValidate()
        {
            _enemyManager = GetComponent<EnemyManager>();
            _spawnTimer = GetComponent<EnemySpawnTimer>();

            _switchComponent = GetComponent<SwitchStateComponent>();
        }

        private void Enable() => _spawnTimer.OnTimeToSpawn += SpawnEnemy;

        private void Disable() => _spawnTimer.OnTimeToSpawn -= SpawnEnemy;

        void IGameUpdateListener.OnUpdate() =>
            _spawnTimer.TimerCountdown(_enemyManager.ReservationsAmount, _gameManager.CurrentGameState);

        private void SpawnEnemy() => _enemyManager.SpawnEnemy();

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