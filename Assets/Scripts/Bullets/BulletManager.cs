using System.Collections.Generic;
using ShootEmUp.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp
{
    [RequireComponent(typeof(BulletSpawner))]
    public sealed class BulletManager : MonoBehaviour, IGameFixedUpdateListener, IBulletSpawner, IBulletUnspawner
    {
        [SerializeField] private LevelBounds _levelBounds;

        [SerializeField] private BulletSpawner bulletSpawner;

        private readonly List<Bullet> _bullets = new List<Bullet>();

        private void OnValidate() => bulletSpawner = GetComponent<BulletSpawner>();

        void IGameFixedUpdateListener.OnFixedUpdate()
        {
            for (int i = 0; i < _bullets.Count; i++)
            {
                Bullet currentBullet = _bullets[i];

                if (_levelBounds.InBounds(currentBullet.Position))
                    return;

                UnspawnBullet(currentBullet);
            }
        }

        private void UnspawnBullet(Bullet bullet)
        {
            _bullets.Remove(bullet);
            bulletSpawner.UnspawnBullet(bullet);
        }
        
        void IBulletSpawner.SpawnBullet(Args args) => _bullets.Add(bulletSpawner.SpawnBullet(args));

        void IBulletUnspawner.UnspawnBullet(Bullet bullet) => UnspawnBullet(bullet);
    }
}
