using System;
using UnityEngine;

namespace ShootEmUp
{
    public class EnemySpawnTimer : MonoBehaviour
    {
        public event Action OnTimeToSpawn;
        
        private const int TimeBtwEnemySpawn = 1;
        private float _currentTime = TimeBtwEnemySpawn;
        
        private int _enemiesSpawned;
        
        public void TimerCountdown(int reservationAmount)
        {
            if (_enemiesSpawned >= reservationAmount)
                return;
            
            if (_currentTime > 0)
            {
                _currentTime -= Time.deltaTime;
                return;
            }
            
            OnTimeToSpawn?.Invoke();

            _enemiesSpawned++;
            ResetValues();
        }

        private void ResetValues() => _currentTime = TimeBtwEnemySpawn;
    }
}