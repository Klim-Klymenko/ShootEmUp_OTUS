using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public sealed class BulletSpawner : IGameInitializeListener
    {
        [SerializeField]
        private int _reservationAmount = 1000;
        
        [SerializeField]
        private Bullet _prefab;
        
        [SerializeField]
        private Transform _parentToGet;
        
        [SerializeField]
        private Transform _parentToPut;
        
        private Pool<Bullet> _bulletPool;
        
        private GameManager _gameManager;

        [Inject]
        public void Construct(GameManager gameManager) 
        {
            _gameManager = gameManager;
        }

        void IGameInitializeListener.OnInitialize() => InitializePool();
        
        private void InitializePool()
        {
            _bulletPool ??= new Pool<Bullet>(_reservationAmount, _prefab, _parentToGet, _parentToPut);
            _bulletPool.Reserve();
        }
        
        public Bullet SpawnBullet(Args args)
        {
            if (_bulletPool == null)
                throw new Exception("Pull hasn't been allocated");
            
            Bullet bullet = _bulletPool.Get();
            
            bullet.Position = args.Position;
            bullet.Color = args.Color;
            bullet.PhysicsLayer = args.PhysicsLayer;
            bullet.Damage = args.Damage;
            bullet.CohesionType = args.CohesionType;
            bullet.Velocity = args.Velocity;
            
            _gameManager.AddGameListeners(bullet.Resolve<IGameListener[]>());
            
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