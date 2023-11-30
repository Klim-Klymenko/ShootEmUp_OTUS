using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(SwitchStateComponent))]
    [RequireComponent(typeof(HitPointsComponent))]
    [RequireComponent(typeof(MoveComponent))]
    [RequireComponent(typeof(EnemyAttackAgent))]
    [RequireComponent(typeof(EnemyAttackTimer))]
    public sealed class EnemyAttackController : MonoBehaviour, IGameFixedUpdateListener,
        IGameStartListener, IGameFinishListener, IGameResumeListener, IGamePauseListener
    {
        [SerializeField] private HitPointsComponent _hitPointsComponent;
        [SerializeField] private EnemyMoveAgent _enemyMove;
        [SerializeField] private MoveComponent _moveComponent;
        
        [SerializeField] private EnemyAttackAgent _attackAgent;
        [SerializeField] private EnemyAttackTimer _enemyTimer;

        private IBulletSpawner _bulletManager;

        public IBulletSpawner BulletManager
        {
            set => _bulletManager = value;
        }
        
        [SerializeField] private SwitchStateComponent _switchComponent;
        
        public bool IsOnlyUnityMethods { get; } = false;
        
        private void OnValidate()
        {
            _hitPointsComponent = GetComponent<HitPointsComponent>();
            _enemyMove = GetComponent<EnemyMoveAgent>();
            _moveComponent = GetComponent<MoveComponent>();
            
            _attackAgent = GetComponent<EnemyAttackAgent>();
            _enemyTimer = GetComponent<EnemyAttackTimer>();

            _switchComponent = GetComponent<SwitchStateComponent>();
        }

        private void Enable() => _enemyTimer.OnTimeToShoot += Fire;

        private void Disable() => _enemyTimer.OnTimeToShoot -= Fire;

        void IGameFixedUpdateListener.OnFixedUpdate() => 
            _enemyTimer.TimerCountdown(_enemyMove.IsReached, _hitPointsComponent.AnyHitPoints);

        private void Fire() => _attackAgent.Fire(_moveComponent.Position, _bulletManager);
        
        public void OnStart()
        {
            _switchComponent.TurnOn(this);

            Enable();
        }
        
        void IGameFinishListener.OnFinish()
        {
            _switchComponent.TurnOff(this);
            
            Disable();
        }

        void IGameResumeListener.OnResume()
        {
            _switchComponent.TurnOn(this);
            
            Enable();
        }
        
        void IGamePauseListener.OnPause()
        {
            _switchComponent.TurnOff(this);
            
            Disable();
        }
    }
}