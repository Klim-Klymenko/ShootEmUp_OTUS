using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(EnemySpawner))]
    [RequireComponent(typeof(EnemySpawnTimer))]
    public sealed class EnemySpawnController : MonoBehaviour, IGameUpdateListener,
        IGameStartListener, IGameFinishListener, IGameResumeListener, IGamePauseListener
    {
        [SerializeField] private GameManager _gameManager;
        
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private EnemySpawnTimer _spawnTimer;
        
        private void OnValidate()
        {
            _spawnTimer = GetComponent<EnemySpawnTimer>();
            _enemySpawner = GetComponent<EnemySpawner>();
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