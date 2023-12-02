﻿using UnityEngine;

namespace ShootEmUp
{
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

        private BulletManager _bulletManager;

        public BulletManager BulletManager
        {
            set => _bulletManager = value;
        }

        private void OnValidate()
        {
            _hitPointsComponent = GetComponent<HitPointsComponent>();
            _enemyMove = GetComponent<EnemyMoveAgent>();
            _moveComponent = GetComponent<MoveComponent>();
            
            _attackAgent = GetComponent<EnemyAttackAgent>();
            _enemyTimer = GetComponent<EnemyAttackTimer>();
        }

        private void Enable() => _enemyTimer.OnTimeToShoot += Fire;

        private void Disable() => _enemyTimer.OnTimeToShoot -= Fire;

        void IGameFixedUpdateListener.OnFixedUpdate() => 
            _enemyTimer.TimerCountdown(_enemyMove.IsReached, _hitPointsComponent.AnyHitPoints);

        private void Fire() => _attackAgent.Fire(_moveComponent.Position, _bulletManager);
        
        void IGameStartListener.OnStart() => Enable();

        void IGameFinishListener.OnFinish() => Disable();

        void IGameResumeListener.OnResume() => Enable();
        
        void IGamePauseListener.OnPause() => Disable();
    }
}