using UnityEngine;

namespace ShootEmUp
{
    public class EnemyAttackController : MonoBehaviour
    {
        [SerializeField] private HitPointsComponent _hitPointsComponent;
        [SerializeField] private EnemyMoveAgent _enemyMove;
        [SerializeField] private MoveComponent _moveComponent;
        
        [SerializeField] private EnemyAttackAgent _attackAgent;
        [SerializeField] private EnemyAttackTimer _enemyTimer;

        private void OnValidate()
        {
            _hitPointsComponent = GetComponent<HitPointsComponent>();
            _enemyMove = GetComponent<EnemyMoveAgent>();
            _moveComponent = GetComponent<MoveComponent>();
            
            _attackAgent = GetComponent<EnemyAttackAgent>();
            _enemyTimer = GetComponent<EnemyAttackTimer>();
        }

        private void OnEnable() => _enemyTimer.OnTimeToShoot += Fire;

        private void OnDisable() => _enemyTimer.OnTimeToShoot -= Fire;

        private void FixedUpdate() => 
            _enemyTimer.TimerCountdown(_enemyMove.IsReached, _hitPointsComponent.AnyHitPoints);

        private void Fire() => _attackAgent.Fire(_moveComponent.Position);
    }
}