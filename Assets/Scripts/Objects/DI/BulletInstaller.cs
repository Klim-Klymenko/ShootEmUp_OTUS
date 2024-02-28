using Common;
using UnityEngine;
using Zenject;

namespace Objects
{
    internal sealed class BulletInstaller : MonoInstaller
    {
        [SerializeField]
        private int _reservationAmount;
        
        [SerializeField]
        private Bullet _prefab;
        
        [SerializeField]
        private Transform _poolContainer;
        
        [SerializeField]
        private Transform _firePoint;
        
        public override void InstallBindings()
        {
            BindPool();
            BindFactory();
        }
        
        private void BindPool()
        {
            Container.Bind<Pool<Bullet>>().AsSingle().WithArguments(_reservationAmount, _prefab, _poolContainer);
        }
        
        private void BindFactory()
        {
            Container.Bind<ISpawner<Transform>>().To<BulletSpawner>().AsSingle().WithArguments(_firePoint);
        }
    }
}