using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletInstaller : DependencyInstaller
    {
        [SerializeField, Service, Listener]
        private BulletSpawner _bulletSpawner;
        
        [Service, Listener, Interfaces(typeof(IBulletUnspawner), typeof(IBulletSpawner))]
        private BulletManager _bulletManager = new();
    }
}