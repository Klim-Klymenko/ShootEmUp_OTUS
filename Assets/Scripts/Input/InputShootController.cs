using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputShootController : IGameUpdateListener
    {
        private CharacterBulletShooter _bulletShooter;
        
        [Inject]
        private void Construct(CharacterBulletShooter bulletShooter)
        {
            _bulletShooter = bulletShooter;
        }

        void IGameUpdateListener.OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _bulletShooter.ShootBullet();
        }
    }
}