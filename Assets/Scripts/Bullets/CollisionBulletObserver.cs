using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(Bullet))]
    public sealed class CollisionBulletObserver : MonoBehaviour, IGameStartListener,
        IGameFinishListener, IGameResumeListener, IGamePauseListener
    {
        [SerializeField] private Bullet _bullet;
        
        [HideInInspector] public BulletManager BulletManager;

        public bool IsOnlyUnityMethods { get; } = false;

        private void OnValidate() => _bullet = GetComponent<Bullet>();
        
        private void Enable() => _bullet.OnCollisionEntered += BulletShot;

        private void Disable() => _bullet.OnCollisionEntered -= BulletShot;

        private void BulletShot(Bullet bullet, Collision2D collision2D) => 
            BulletManager.BulletShot(bullet, collision2D);

        public void OnStart() => Enable();

        void IGameFinishListener.OnFinish() => Disable();

        void IGameResumeListener.OnResume() => Enable();

        void IGamePauseListener.OnPause() => Disable();
    }
}
