using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterInstaller : DependencyInstaller
    {
        [SerializeField, Service]
        private MoveComponent _moveComponent;
        
        [SerializeField, Service]
        private WeaponComponent _weaponComponent;

        [SerializeField, Service]
        private HitPointsComponent _hitPointsComponent;
        
        [SerializeField, Service]
        private CharacterBulletShooter _characterBulletShooter;
        
        [Listener]
        private CharacterHitPointsObserver _characterHitPointsObserver = new();
        
        [Listener]
        private InputMoveController _inputMoveController = new();
    
        [Listener]
        private InputShootController _inputShootController = new();
    }
}