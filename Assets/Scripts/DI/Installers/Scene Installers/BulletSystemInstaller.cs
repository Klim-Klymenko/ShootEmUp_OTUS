using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystemInstaller : Installer
    {
        [SerializeField, Service, Listener]
        private BulletSpawner _bulletSpawner;
        
        [Service(typeof(IBulletUnspawner), typeof(IBulletSpawner)), Listener]
        private BulletManager _bulletManager = new();
    }
}