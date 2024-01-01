using UnityEngine;

namespace ShootEmUp
{
    [System.Serializable]
    public sealed class EnemyService
    {
        [SerializeField]
        private GameObject _gameObject;
        
        [SerializeField]
        private Transform _transform;
        
        [SerializeField]
        private HitPointsComponent _hitPointsComponent;
        
        [SerializeField]
        private MoveComponent _moveComponent;

        public GameObject GameObject => _gameObject;
        public Transform Transform => _transform;
        public HitPointsComponent HitPointsComponent => _hitPointsComponent;
        public MoveComponent MoveComponent => _moveComponent;
    }
}