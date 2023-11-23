using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyReferenceComponent : MonoBehaviour, IPoolable
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private GameObject _gameObject;
        [SerializeField] private EnemyAttackAgent _attackAgent;
        [SerializeField] private EnemyMoveAgent _moveAgent;
        [SerializeField] private EnemyAttackController _attackController;
        
        public Transform Transform => _transform;
        public GameObject GameObject => _gameObject;
        public EnemyAttackAgent AttackAgent => _attackAgent;
        public EnemyMoveAgent MoveAgent => _moveAgent;
        public EnemyAttackController AttackController => _attackController;

        public EnemyManager EnemyManager { get; set; }

        private void OnValidate()
        {
            _transform = transform;
            _gameObject = gameObject;
            _attackAgent = GetComponent<EnemyAttackAgent>();
            _moveAgent = GetComponent<EnemyMoveAgent>();
            _attackController = GetComponent<EnemyAttackController>();
        }
    }
}