using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletInstaller : Installer
    {
        [SerializeField, Service]
        private BulletService _bulletService;

        [SerializeField, Service, Listener]
        private Bullet _bullet;

        [Listener]
        private BulletDestructionObserver _destructionObserver;
        
        public override IEnumerable<object> ProvideServices()
        {
            yield return _bulletService;
            yield return _bullet;
            yield return CombineListeners();
        }

        public override IEnumerable<object> ProvideInjectables()
        {
            yield return _bullet;
            yield return CreateOrProviderDestructionObserver();
        }

        private BulletDestructionObserver CreateOrProviderDestructionObserver() =>
            _destructionObserver ??= new BulletDestructionObserver(_bullet);
        
        private IEnumerable<IGameListener> CombineListeners() => new IGameListener[]
        {
            CreateOrProviderDestructionObserver(), _bullet
        };
    }
}