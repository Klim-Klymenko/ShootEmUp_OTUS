using UnityEngine;

namespace ShootEmUp
{
    [System.Serializable]
    public sealed class CharacterBulletShooter
    {
        [SerializeField]
        private Color _bulletColour;
        
        [SerializeField]
        private int _bulletDamage;
        
        [SerializeField]
        private float _bulletSpeed;

        private IBulletSpawner _bulletSpawner;
        private WeaponComponent _weaponComponent;

        [Inject]
        public void Construct(IBulletSpawner bulletSpawner, CharacterService characterService)
        {
            _bulletSpawner = bulletSpawner;
            _weaponComponent = characterService.WeaponComponent;
        }

        public void ShootBullet()
        {
            _bulletSpawner.SpawnBullet(new Args 
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