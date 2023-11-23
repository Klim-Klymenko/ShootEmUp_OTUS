using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(SwitchStateComponent))]
    public sealed class BulletManager : MonoBehaviour, IGameFixedUpdateListener, IGameStartListener,
        IGameFinishListener, IGameResumeListener, IGamePauseListener
    {
        [SerializeField] private LevelBounds _levelBounds;

        [SerializeField] private BulletBuilder _bulletBuilder;

        [SerializeField] private SwitchStateComponent _switchComponent;
        
        private readonly List<Bullet> _bullets = new List<Bullet>();

        public bool IsOnlyUnityMethods { get; } = false;

        private void OnValidate()
        {
            _bulletBuilder = GetComponent<BulletBuilder>();
            _switchComponent = GetComponent<SwitchStateComponent>();
        }

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
        
        public void OnStart() => _switchComponent.TurnOn(this);

        public void OnFinish() => _switchComponent.TurnOff(this);

        public void OnResume() => _switchComponent.TurnOn(this);

        public void OnPause() => _switchComponent.TurnOff(this);
        
    }
}
