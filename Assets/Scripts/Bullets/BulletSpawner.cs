using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public sealed class BulletSpawner : IGameInitializeListener
    {
        [SerializeField] private int _reservationAmount = 1000;
        [SerializeField] private Bullet _prefab;
        [SerializeField] private Transform _parentToGet;
        [SerializeField] private Transform _parentToPut;

        private readonly List<object> _createdBulletObjects = new();
        
        private Pool<Bullet> _bulletPool;
        
        private GameManager _gameManager;
        private DependencyAssembler _dependencyAssembler;

        [Inject]
        private void Construct(GameManager gameManager, DependencyAssembler dependencyAssembler) 
        {
            _gameManager = gameManager;
            _dependencyAssembler = dependencyAssembler;
        }

        void IGameInitializeListener.OnInitialize() => InitializePool();
        
        public void InitializePool()
        {
            _bulletPool ??= new Pool<Bullet>(_reservationAmount, _prefab, _parentToGet, _parentToPut);
            _bulletPool.Reserve();
        }
        
        public Bullet SpawnBullet(Args args)
        {
            if (_bulletPool == null)
                throw new Exception("Pull hasn't been allocated");
            
            Bullet bullet = _bulletPool.Get();

            _createdBulletObjects.Add(bullet);
            
            bullet.Position = args.Position;
            bullet.Color = args.Color;
            bullet.PhysicsLayer = args.PhysicsLayer;
            bullet.Damage = args.Damage;
            bullet.CohesionType = args.CohesionType;
            bullet.Velocity = args.Velocity;

            _createdBulletObjects.Add(new BulletDestructionObserver(bullet));
            
            for (int i = 0; i < _createdBulletObjects.Count; i++)
            {
                _dependencyAssembler.Inject(_createdBulletObjects[i]);
                
                if (_createdBulletObjects[i] is not IGameListener gameListener) continue;
                
                _gameManager.AddGameListener(gameListener);
                
                if  (gameListener is IGameStartListener startListener)
                    startListener.OnStart();
            }
            return bullet;
        }

        public void UnspawnBullet(Bullet bullet)
        {
            if (_bulletPool == null)
                throw new Exception("Pull hasn't been allocated");

            _bulletPool.Put(bullet);
        }
    }
}