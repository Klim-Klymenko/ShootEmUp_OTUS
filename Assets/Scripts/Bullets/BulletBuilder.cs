using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(BulletManager))]
    public sealed class BulletBuilder : MonoBehaviour
    {
        [SerializeField] private int _reservationAmount = 1000;
        [SerializeField] private Bullet _prefab;
        [SerializeField] private Transform _parentToGet;
        [SerializeField] private  Transform _parentToPut;
        [SerializeField] private BulletManager _manager;
        [SerializeField] private GameManager _gameManager;
        
        private Pool<Bullet> _bulletPool;

        private void OnValidate() => _manager = GetComponent<BulletManager>();

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

            SwitchStateComponent switchStateComponent = bullet.GetComponent<SwitchStateComponent>();
            switchStateComponent.GameManager = _gameManager;
            
            CollisionBulletObserver collisionObserver = bullet.GetComponent<CollisionBulletObserver>();
            collisionObserver.BulletManager = _manager;
            _gameManager.AddEventListeners(collisionObserver);
            collisionObserver.OnStart();

            bullet.SwitchComponent = switchStateComponent;
            _gameManager.AddEventListeners(bullet);
            bullet.OnStart();

            return bullet;
        }

        public void UnspawnBullet(Bullet bullet)
        {
            if (_bulletPool == null)
                throw new Exception("Pull hasn't been allocated");
            
            _bulletPool.Put(bullet);
        }

        public void InitializePool()
        {
            _bulletPool ??= new Pool<Bullet>(_reservationAmount, _prefab, _parentToGet, _parentToPut);
            _bulletPool.Reserve();
        }
    }
}