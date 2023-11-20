using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletManager : MonoBehaviour
    {
        [SerializeField] private LevelBounds _levelBounds;

        [SerializeField] private BulletBuilder _bulletBuilder;
        
        private readonly List<Bullet> _bullets = new List<Bullet>();

        private void OnValidate() => _bulletBuilder = GetComponent<BulletBuilder>();

        private void FixedUpdate()
        {
            for (int i = 0; i < _bullets.Count; i++)
            {
                Bullet currentBullet = _bullets[i];

                if (_levelBounds.InBounds(currentBullet.Position))
                    return;

                UnspawnBullet(currentBullet);
            }
        }

        public void SpawnBullet(Args args) =>_bullets.Add(_bulletBuilder.SpawnBullet(args));
        
        private void UnspawnBullet(Bullet bullet)
        {
            _bullets.Remove(bullet);
            _bulletBuilder.UnspawnBullet(bullet);
        }

        public void BulletShot(Bullet bullet, Collision2D collision)
        {
            if (!collision.gameObject.TryGetComponent(out TeamComponent teamComponent))
                return;
            
            if (teamComponent.CohesionType == bullet.CohesionType)
                return;
            
            if (!collision.gameObject.TryGetComponent(out HitPointsComponent hitPointsComponent))
                return;
            
            hitPointsComponent.TakeDamage(bullet.Damage);
            UnspawnBullet(bullet);
        }
    }
}
