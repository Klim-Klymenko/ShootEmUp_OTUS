using System;
using UnityEngine;

namespace ShootEmUp
{
    public class EnemyBuilder : MonoBehaviour
    {
        [SerializeField] private  int _reservationAmount = 7;
        [SerializeField] private EnemyReferenceComponent _prefab;
        [SerializeField] private Transform _parentToGet;
        [SerializeField] private Transform _parentToPut;
        
        [SerializeField] private Transform _target;
        [SerializeField] private EnemyPositionsGenerator _randomPositionGenerator;
        [SerializeField] private EnemyManager _enemyManager;

        private Pool<EnemyReferenceComponent> _enemyPool;

        public int ReservationAmount => _reservationAmount;

        private void Awake() => InitializePool();

        public EnemyReferenceComponent SpawnEnemy()
        {
            if (_enemyPool == null)
                throw new Exception("Pull hasn't been allocated");

            var enemy = _enemyPool.Get();

            enemy.Transform.position = _randomPositionGenerator.RandomSpawnPosition();
            enemy.MoveAgent.Destination = _randomPositionGenerator.RandomAttackPosition();

            enemy.EnemyManager = _enemyManager;
            enemy.AttackAgent.Target = _target;

            return enemy;
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