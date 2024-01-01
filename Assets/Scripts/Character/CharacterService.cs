using UnityEngine;

namespace ShootEmUp
{
    [System.Serializable]
    public sealed class CharacterService
    {
        [SerializeField] 
        private GameObject _gameObject;
        
        [SerializeField]
        private Transform _transform;
        
        [SerializeField]
        private MoveComponent _moveComponent;

        [SerializeField]
        private HitPointsComponent _hitPointsComponent;

        [SerializeField]
        private WeaponComponent _weaponComponent;

        [SerializeField]
        private TeamComponent _teamComponent;
        
        public GameObject GameObject => _gameObject;
        public Transform Transform => _transform;
        public MoveComponent MoveComponent => _moveComponent;
        public HitPointsComponent HitPointsComponent => _hitPointsComponent;
        public WeaponComponent WeaponComponent => _weaponComponent;
        public TeamComponent TeamComponent => _teamComponent;
    }
}