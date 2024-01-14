namespace ShootEmUp
{
    public sealed class BulletDestructionObserver : IGameStartListener,
        IGameFinishListener, IGameResumeListener, IGamePauseListener
    {
        private readonly Bullet _bullet;
        private IBulletUnspawner _bulletUnspawner;

        public BulletDestructionObserver(Bullet bullet)
        {
            _bullet = bullet;
        }

        [Inject]
        public void Construct(IBulletUnspawner bulletUnspawner)
        {
            _bulletUnspawner = bulletUnspawner;
        }
        
        private void Enable() => _bullet.OnBulletDestroyed += UnspawnBullet;

        private void Disable() => _bullet.OnBulletDestroyed -= UnspawnBullet;

        private void UnspawnBullet() => _bulletUnspawner.UnspawnBullet(_bullet);
        
        void IGameStartListener.OnStart() => Enable();

        void IGameFinishListener.OnFinish() => Disable();

        void IGameResumeListener.OnResume() => Enable();

        void IGamePauseListener.OnPause() => Disable();
    }
}