using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyBuilder : MonoBehaviour
    {
        [SerializeField] private  int _reservationAmount = 7;
        [SerializeField] private EnemyReferenceComponent _prefab;
        [SerializeField] private Transform _parentToGet;
        [SerializeField] private Transform _parentToPut;
        
        [SerializeField] private Transform _target;
        [SerializeField] private EnemyPositionsGenerator _randomPositionGenerator;
        [SerializeField] private EnemyManager _enemyManager;

        [SerializeField] private GameManager _gameManager;
        
        private Pool<EnemyReferenceComponent> _enemyPool;

        public int ReservationAmount => _reservationAmount;

        private void Awake() => InitializePool();

        public void SpawnEnemy()
        {
            if (_enemyPool == null)
                throw new Exception("Pull hasn't been allocated");

            var enemy = _enemyPool.Get();

            enemy.Transform.position = _randomPositionGenerator.RandomSpawnPosition();
            enemy.MoveAgent.Destination = _randomPositionGenerator.RandomAttackPosition();

            SwitchStateComponent switchComponent = enemy.GetComponent<SwitchStateComponent>();
            switchComponent.GameManager = _gameManager;
            
            enemy.MoveAgent.OnStart();
            _gameManager.AddUpdateListeners(enemy.MoveAgent);
            _gameManager.AddEventListeners(enemy.MoveAgent);
            
            enemy.AttackController.OnStart();
            _gameManager.AddUpdateListeners(enemy.AttackController);
            _gameManager.AddEventListeners(enemy.AttackController);

            EnemyShootingController shootingController = enemy.GetComponent<EnemyShootingController>();
            shootingController.OnStart();
            _gameManager.AddEventListeners(shootingController);

            EnemyDeathObserver deathObserver = enemy.GetComponent<EnemyDeathObserver>();
            deathObserver.OnStart();
            _gameManager.AddEventListeners(deathObserver);
            
            enemy.EnemyManager = _enemyManager;
            enemy.AttackAgent.Target = _target;
        }

        public void UnspawnEnemy(EnemyReferenceComponent enemy)
        {
            if (_enemyPool == null)
                throw new Exception("Pull hasn't been allocated");
            
            _enemyPool.Put(enemy);
        }

        private void InitializePool()
        {
            _enemyPool ??= new Pool<EnemyReferenceComponent>(_reservationAmount, _prefab, _parentToGet, _parentToPut);
            _enemyPool.Reserve();
        }
    }
}