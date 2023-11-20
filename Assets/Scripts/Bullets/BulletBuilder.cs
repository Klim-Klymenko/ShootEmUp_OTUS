using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletBuilder : MonoBehaviour
    {
        [SerializeField] private int _reservationAmount = 1000;
        [SerializeField] private Bullet _prefab;
        [SerializeField] private Transform _parentToGet;
        [SerializeField] private  Transform _parentToPut;
        [SerializeField] private BulletManager _manager;

        private Pool<Bullet> _bulletPool;

        private void OnValidate() => _manager = GetComponent<BulletManager>();

        private void Awake() => InitializePool();

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

            bullet.GetComponent<CollisionBulletObserver>().BulletManager = _manager;

            return bullet;
        }

        public void UnspawnBullet(Bullet bullet)
        {
            if (_bulletPool == null)
                throw new Exception("Pull hasn't been allocated");
            
            _bulletPool.Put(bullet);
        }

        private void InitializePool()
        {
            _bulletPool ??= new Pool<Bullet>(_reservationAmount, _prefab, _parentToGet, _parentToPut);
            _bulletPool.Reserve();
        }
    }
}