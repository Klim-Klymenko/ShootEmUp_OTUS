using System.Collections.Generic;

namespace ShootEmUp
{
    public sealed class BulletManager : IGameFixedUpdateListener, IBulletSpawner, IBulletUnspawner
    {
        private readonly List<Bullet> _bullets = new();
        
        private LevelBounds _levelBounds;
        private BulletSpawner _bulletSpawner;

        [Inject]
        public void Construct(LevelBounds levelBounds, BulletSpawner bulletSpawner)
        {
            _levelBounds = levelBounds;
            _bulletSpawner = bulletSpawner;
        }

        void IGameFixedUpdateListener.OnFixedUpdate()
        {
            for (int i = 0; i < _bullets.Count; i++)
            {
                Bullet currentBullet = _bullets[i];

                if (_levelBounds.InBounds(currentBullet.Position)) return;

                UnspawnBullet(currentBullet);
            }
        }

        void IBulletSpawner.SpawnBullet(Args args) => _bullets.Add(_bulletSpawner.SpawnBullet(args));

        public void UnspawnBullet(Bullet bullet)
        {
            _bullets.Remove(bullet);
            _bulletSpawner.UnspawnBullet(bullet);
        }
    }
}
