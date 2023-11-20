using UnityEngine;

namespace ShootEmUp
{
    public class EnemySpawnController : MonoBehaviour
    {
        [SerializeField] private EnemyManager _enemyManager;
        [SerializeField] private EnemySpawnTimer _spawnTimer;

        private void OnValidate()
        {
            _enemyManager = GetComponent<EnemyManager>();
            _spawnTimer = GetComponent<EnemySpawnTimer>();
        }

        private void OnEnable() => _spawnTimer.OnTimeToSpawn += SpawnEnemy;

        private void OnDisable() => _spawnTimer.OnTimeToSpawn -= SpawnEnemy;

        private void Update() => _spawnTimer.TimerCountdown(_enemyManager.ReservationsAmount);

        private void SpawnEnemy() => _enemyManager.SpawnEnemy();
    }
}