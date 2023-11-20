using UnityEngine;

namespace ShootEmUp
{
    public class CollisionBulletObserver : MonoBehaviour
    {
        [SerializeField] private Bullet _bullet;
        
        [HideInInspector] public BulletManager BulletManager;

        private void OnValidate() => _bullet = GetComponent<Bullet>();
        
        private void OnEnable() => _bullet.OnCollisionEntered += BulletShot;

        private void OnDisable() => _bullet.OnCollisionEntered -= BulletShot;

        private void BulletShot(Bullet bullet, Collision2D collision2D) => 
            BulletManager.BulletShot(bullet, collision2D);
    }
}
