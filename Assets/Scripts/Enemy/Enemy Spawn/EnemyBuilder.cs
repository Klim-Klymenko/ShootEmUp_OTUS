using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(EnemyManager))]
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

        private void OnValidate() => _enemyManager = GetComponent<EnemyManager>();

        public void SpawnEnemy()
        {
            if (_enemyPool == null)
                throw new Exception("Pull hasn't been allocated");

            var enemy = _enemyPool.Get();

            enemy.Transform.position = _randomPositionGenerator.RandomSpawnPosition();
            enemy.MoveAgent.Destination = _randomPositionGenerator.RandomAttackPosition();

            enemy.EnemyManager = _enemyManager;
            enemy.AttackAgent.Target = _target;

            SwitchStateComponent switchComponent = enemy.GetComponent<SwitchStateComponent>();
            switchComponent.GameManager = _gameManager;

            _gameManager.AddEventListeners(enemy.MoveAgent);
            enemy.MoveAgent.OnStart();

            _gameManager.AddEventListeners(enemy.AttackController);
            enemy.AttackController.OnStart();

            EnemyShootingController shootingController = enemy.GetComponent<EnemyShootingController>();
            _gameManager.AddEventListeners(shootingController);
            shootingController.OnStart();

            EnemyDeathObserver deathObserver = enemy.GetComponent<EnemyDeathObserver>();
            _gameManager.AddEventListeners(deathObserver);
            deathObserver.OnStart();
        }

        public void UnspawnEnemy(EnemyReferenceComponent enemy)
        {
            if (_enemyPool == null)
                throw new Exception("Pull hasn't been allocated");
            
            _enemyPool.Put(enemy);
        }

        public void InitializePool()
        {
            _enemyPool ??= new Pool<EnemyReferenceComponent>(_reservationAmount, _prefab, _parentToGet, _parentToPut);
            _enemyPool.Reserve();
        }
    }
}