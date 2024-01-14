namespace ShootEmUp
{
    public sealed class EnemyAttackController : IGameFixedUpdateListener,
        IGameStartListener, IGameFinishListener, IGameResumeListener, IGamePauseListener
    {
        private readonly EnemyAttackTimer _enemyTimer = new();
        
        private readonly HitPointsComponent _hitPointsComponent;
        private readonly MoveComponent _moveComponent;
        private readonly EnemyMoveAgent _enemyMove;
        private readonly EnemyAttackAgent _attackAgent;

        private IBulletSpawner _bulletSpawner;

        public EnemyAttackController(EnemyService enemyService, EnemyMoveAgent enemyMove, EnemyAttackAgent attackAgent)
        {
            _hitPointsComponent = enemyService.HitPointsComponent;
            _moveComponent = enemyService.MoveComponent;
            _enemyMove = enemyMove;
            _attackAgent = attackAgent;
        }
        
        [Inject]
        public void Construct(IBulletSpawner bulletSpawner)
        {
            _bulletSpawner = bulletSpawner;
        }
        
        private void Enable() => _enemyTimer.OnTimeToShoot += Fire;

        private void Disable() => _enemyTimer.OnTimeToShoot -= Fire;

        void IGameFixedUpdateListener.OnFixedUpdate() => 
            _enemyTimer.TimerCountdown(_enemyMove.IsReached, _hitPointsComponent.AnyHitPoints);

        private void Fire() => _attackAgent.Fire(_moveComponent.Position, _bulletSpawner);
        
        void IGameStartListener.OnStart() => Enable();

        void IGameFinishListener.OnFinish() => Disable();

        void IGameResumeListener.OnResume() => Enable();
        
        void IGamePauseListener.OnPause() => Disable();
    }
}