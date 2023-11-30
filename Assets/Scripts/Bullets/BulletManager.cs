using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(SwitchStateComponent))]
    [RequireComponent(typeof(BulletSpawner))]
    public sealed class BulletManager : MonoBehaviour, IBulletSpawner, IGameFixedUpdateListener, IGameStartListener,
        IGameFinishListener, IGameResumeListener, IGamePauseListener
    {
        [SerializeField] private LevelBounds _levelBounds;

        [SerializeField] private BulletSpawner bulletSpawner;

        [SerializeField] private SwitchStateComponent _switchComponent;
        
        private readonly List<Bullet> _bullets = new List<Bullet>();

        public bool IsOnlyUnityMethods { get; } = false;

        private void OnValidate()
        {
            bulletSpawner = GetComponent<BulletSpawner>();
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

        public void SpawnBullet(Args args) => _bullets.Add(bulletSpawner.SpawnBullet(args));
        
        public void UnspawnBullet(Bullet bullet)
        {
            _bullets.Remove(bullet);
            bulletSpawner.UnspawnBullet(bullet);
        }

        void IGameStartListener.OnStart() => _switchComponent.TurnOn(this);

        void IGameFinishListener.OnFinish() => _switchComponent.TurnOff(this);

        void IGameResumeListener.OnResume() => _switchComponent.TurnOn(this);

        void IGamePauseListener.OnPause() => _switchComponent.TurnOff(this);
    }
}
