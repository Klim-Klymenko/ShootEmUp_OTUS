using Atomic.Elements;
using Atomic.Extensions;
using Common;
using GameCycle;
using GameEngine;
using JetBrains.Annotations;
using UnityEngine;

namespace Objects
{
    [UsedImplicitly]
    public sealed class BulletSpawner : ISpawner<Transform>
    {
        private readonly Pool<Bullet> _pool;
        private readonly GameCycleManager _gameCycleManager;
        private readonly Transform _firePoint;
        
        public BulletSpawner(Pool<Bullet> pool, GameCycleManager gameCycleManager, Transform firePoint)
        {
            _pool = pool;
            _gameCycleManager = gameCycleManager;
            _firePoint = firePoint;
        }

        Transform ISpawner<Transform>.Spawn()
        {
            Bullet bullet = _pool.Get();
            Transform bulletTransform = bullet.transform;

            bulletTransform.position = _firePoint.position;
            bulletTransform.forward = _firePoint.forward;
            
            bullet.Compose();

            IAtomicObservable destroyEvent = bullet.GetObservable(LiveableAPI.DeathObservable);
            
            destroyEvent.Subscribe(() => Despawn(bulletTransform));
            
            if (!_gameCycleManager.ContainsListener(bullet))
                _gameCycleManager.AddListener(bullet);
            
            return bulletTransform;
        }

        public void Despawn(Transform obj)
        {
            Bullet bullet = obj.GetComponent<Bullet>();
            
            bullet.OnFinish();
            _gameCycleManager.RemoveListener(bullet);
            
            _pool.Put(bullet);
        }
    }
}