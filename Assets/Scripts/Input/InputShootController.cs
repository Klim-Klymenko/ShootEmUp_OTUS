using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputShootController : IGameUpdateListener
    {
        private const KeyCode ShootKey = KeyCode.Space;
        
        private CharacterBulletShooter _bulletShooter;
        
        [Inject]
        public void Construct(CharacterBulletShooter bulletShooter)
        {
            _bulletShooter = bulletShooter;
        }

        void IGameUpdateListener.OnUpdate()
        {
            if (Input.GetKeyDown(ShootKey))
                _bulletShooter.ShootBullet();
        }
    }
}