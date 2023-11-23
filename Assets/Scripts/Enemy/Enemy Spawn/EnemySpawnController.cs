using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(SwitchStateComponent))]
    public sealed class EnemySpawnController : MonoBehaviour, IGameUpdateListener,
        IGameStartListener, IGameFinishListener, IGameResumeListener, IGamePauseListener
    {
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

        void IGameUpdateListener.OnUpdate() => _spawnTimer.TimerCountdown(_enemyManager.ReservationsAmount);

        private void SpawnEnemy() => _enemyManager.SpawnEnemy();

        public void OnStart()
        {
            _switchComponent.TurnOn(this);
            Debug.Log("Start");
            Enable();
        }

        public void OnFinish()
        {
            _switchComponent.TurnOff(this);
            
            Disable();
        }

        public void OnResume()
        {
            _switchComponent.TurnOn(this);
            
            Enable();
        }

        public void OnPause()
        {
            _switchComponent.TurnOff(this);
            
            Disable();
        } 
    }
}