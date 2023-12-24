using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public sealed class EnemySpawner : IGameInitializeListener
    {
        public int ReservationAmount => _reservationAmount;
        
        [SerializeField] private  int _reservationAmount = 7;
        [SerializeField] private EnemyReferenceComponent _prefab;
        [SerializeField] private Transform _parentToGet;
        [SerializeField] private Transform _parentToPut;
        
        [SerializeField] private Transform _target;
        
        private Pool<EnemyReferenceComponent> _enemyPool;
        
        private DependencyAssembler _dependencyAssembler;
        private GameManager _gameManager;
        private EnemyPositionsGenerator _randomPositionGenerator;

        [Inject]
        private void Construct(DependencyAssembler dependencyAssembler,
            GameManager gameManager, EnemyPositionsGenerator enemyPositionsGenerator)
        {
            _dependencyAssembler = dependencyAssembler;
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

            foreach (var injectable in enemy.EnemyContextInstaller.ProvideInjectablesWithSceneDependencies())
                _dependencyAssembler.Inject(injectable);
            
            enemy.Transform.position = _randomPositionGenerator.RandomSpawnPosition();
            enemy.EnemyDependencyAssembler.Resolve<EnemyMoveAgent>().Destination = _randomPositionGenerator.RandomAttackPosition();
            enemy.EnemyDependencyAssembler.Resolve<EnemyAttackAgent>().Target = _target;
            
            _gameManager.AddAndStartGameListeners(enemy.EnemyContextInstaller.ProvideGameListeners());
        }

        public void UnspawnEnemy(EnemyReferenceComponent enemy)
        {
            if (_enemyPool == null)
                throw new Exception("Pull hasn't been allocated");
            
            _enemyPool.Put(enemy);
        }
    }
}