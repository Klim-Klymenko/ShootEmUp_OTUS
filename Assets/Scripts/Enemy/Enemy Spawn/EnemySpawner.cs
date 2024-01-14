using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public sealed class EnemySpawner : IGameInitializeListener
    {
        public int ReservationAmount => _reservationAmount;
        
        [SerializeField]
        private  int _reservationAmount = 7;
        
        [SerializeField]
        private EnemyReferenceComponent _prefab;
        
        [SerializeField]
        private Transform _parentToGet;
        
        [SerializeField]
        private Transform _parentToPut;
        
        [SerializeField]
        private Transform _target;
        
        private Pool<EnemyReferenceComponent> _enemyPool;
        
        private GameManager _gameManager;
        private EnemyPositionsGenerator _randomPositionGenerator;

        [Inject]
        public void Construct(GameManager gameManager, EnemyPositionsGenerator enemyPositionsGenerator)
        {
            _gameManager = gameManager;
            _randomPositionGenerator = enemyPositionsGenerator;
        }
        
        void IGameInitializeListener.OnInitialize() => InitializePool();
        
        public void InitializePool()
        {
            _enemyPool ??= new Pool<EnemyReferenceComponent>(_reservationAmount, _prefab, _parentToGet, _parentToPut);
            _enemyPool.Reserve();
        }
        
        public void SpawnEnemy()
        {
            if (_enemyPool == null)
                throw new Exception("Pull hasn't been allocated");

            EnemyReferenceComponent enemy = _enemyPool.Get();
            
            enemy.Position = _randomPositionGenerator.RandomSpawnPosition();
            enemy.Resolve<EnemyMoveAgent>().Destination = _randomPositionGenerator.RandomAttackPosition();
            enemy.Resolve<EnemyAttackAgent>().Target = _target;
            
            _gameManager.AddGameListeners(enemy.Resolve<IGameListener[]>());
        }

        public void UnspawnEnemy(EnemyReferenceComponent enemy)
        {
            if (_enemyPool == null)
                throw new Exception("Pull hasn't been allocated");
            
            _enemyPool.Put(enemy);
        }
    }
}