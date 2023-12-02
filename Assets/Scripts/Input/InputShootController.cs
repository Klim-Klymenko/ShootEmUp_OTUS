using UnityEngine;

namespace ShootEmUp
{
    public class InputShootController : MonoBehaviour, IGameUpdateListener
    {
        [SerializeField] private CharacterBulletShooter _bulletShooter;
        
        void IGameUpdateListener.OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _bulletShooter.ShootBullet();
        }
    }
}