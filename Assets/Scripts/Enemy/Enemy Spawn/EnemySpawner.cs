using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemySpawner : MonoBehaviour, IGameInitializeListener
    {
        [SerializeField] private  int _reservationAmount = 7;
        [SerializeField] private EnemyReferenceComponent _prefab;
        [SerializeField] private Transform _parentToGet;
        [SerializeField] private Transform _parentToPut;
        
        [SerializeField] private Transform _target;
        [SerializeField] private EnemyPositionsGenerator _randomPositionGenerator;

        [SerializeField] private GameManager _gameManager;
        [SerializeField] private BulletManager _bulletManager;
        
        private Pool<EnemyReferenceComponent> _enemyPool;

        public int ReservationAmount => _reservationAmount;

        public void OnInitialize() => InitializePool();
        
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

            enemy.Transform.position = _randomPositionGenerator.RandomSpawnPosition();
            enemy.MoveAgent.Destination = _randomPositionGenerator.RandomAttackPosition();
            
            enemy.AttackAgent.Target = _target;
            enemy.AttackController.BulletManager = _bulletManager;
            enemy.DeathObserver.EnemySpawner = this;
            
            IGameListener[] listeners = enemy.GetComponents<IGameListener>();
            for (int i = 0; i < listeners.Length; i++)
                _gameManager.AddGameListener(listeners[i]);
            
            IGameStartListener[] startListeners = enemy.GetComponents<IGameStartListener>();
            for (int i = 0; i < startListeners.Length; i++)
                startListeners[i].OnStart();
        }

        public void UnspawnEnemy(EnemyReferenceComponent enemy)
        {
            if (_enemyPool == null)
                throw new Exception("Pull hasn't been allocated");
            
            _enemyPool.Put(enemy);
        }
    }
}