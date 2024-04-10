using Common;
using GameCycle;
using UnityEngine;
using Zenject;

namespace EcsEngine.Extensions
{
    internal sealed class EcsEngineInstaller : MonoInstaller
    {
        private ServiceLocator _serviceLocator;
        private EntityManager _entityManager;
        
        public override void InstallBindings()
        {
            BindServiceLocator();
            BindServiceLocatorInstaller();
            BindEntityManager();
            BindEcsStartup();
        }
        
        private void BindServiceLocator()
        {
            Container.Bind<ServiceLocator>().AsSingle();
        }
        
        private void BindServiceLocatorInstaller()
        {
            Container.Bind<ServiceLocatorInstaller>().AsSingle().NonLazy();
        }
        
        private void BindEntityManager()
        {
            Container.Bind<EntityManager>().AsSingle();
        }
        
        private void BindEcsStartup()
        {
            Container.BindInterfacesTo<EcsStartup>().AsSingle();
        }
    }
}