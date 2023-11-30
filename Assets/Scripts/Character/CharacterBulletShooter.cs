using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(WeaponComponent))]
    public sealed class CharacterBulletShooter : MonoBehaviour
    {
        [SerializeField] private Color _bulletColour;
        [SerializeField] private int _bulletDamage;
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private WeaponComponent _weaponComponent;

        [SerializeField] private BulletManager _bulletManager;

        private void OnValidate() => _weaponComponent = GetComponent<WeaponComponent>();
        
        public void ShootBullet()
        {
            _bulletManager.SpawnBullet(new Args 
            {
                CohesionType = CohesionType.Player, 
                PhysicsLayer = (int) PhysicsLayer.PLAYER_BULLET, 
                Color = _bulletColour,
                Damage = _bulletDamage, 
                Position = _weaponComponent.Position, 
                Velocity = _weaponComponent.Rotation * Vector3.up * _bulletSpeed
            });
        }
    }
}