using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private  int _reservationAmount = 7;
        [SerializeField] private Enemy _prefab;
        [SerializeField] private Transform _parentToGet;
        [SerializeField] private Transform _parentToPut;
        
        [SerializeField] private Transform _target;
        [SerializeField] private EnemyPositionsGenerator _randomPositionGenerator;

        [SerializeField] private GameManager _gameManager;
        [SerializeField] private BulletManager _bulletManager;
        
        private Pool<Enemy> _enemyPool;

        public int ReservationAmount => _reservationAmount;

        public void SpawnEnemy()
        {
            if (_enemyPool == null)
                throw new Exception("Pull hasn't been allocated");

            var enemy = _enemyPool.Get();

            enemy.Transform.position = _randomPositionGenerator.RandomSpawnPosition();
            enemy.MoveAgent.Destination = _randomPositionGenerator.RandomAttackPosition();
            
            enemy.AttackAgent.Target = _target;
            
            SwitchStateComponent switchComponent = enemy.GetComponent<SwitchStateComponent>();
            switchComponent.GameManager = _gameManager;

            _gameManager.AddEventListeners(enemy.MoveAgent);
            enemy.MoveAgent.OnStart();

            enemy.AttackController.BulletManager = _bulletManager;
            _gameManager.AddEventListeners(enemy.AttackController);
            enemy.AttackController.OnStart();

            EnemyDeathObserver deathObserver = enemy.GetComponent<EnemyDeathObserver>();
            deathObserver.EnemySpawner = this;
            _gameManager.AddEventListeners(deathObserver);
            deathObserver.OnStart();
        }

        public void UnspawnEnemy(Enemy enemy)
        {
            if (_enemyPool == null)
                throw new Exception("Pull hasn't been allocated");
            
            _enemyPool.Put(enemy);
        }

        public void InitializePool()
        {
            _enemyPool ??= new Pool<Enemy>(_reservationAmount, _prefab, _parentToGet, _parentToPut);
            _enemyPool.Reserve();
        }
    }
}