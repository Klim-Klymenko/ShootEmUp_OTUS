using Common;
using GameCycle;
using UnityEngine;
using Zenject;

namespace EcsEngine
{
    internal sealed class EcsEngineInstaller : MonoInstaller, IFinishGameListener
    {
        [SerializeField] 
        private SceneContext _sceneContext;
        
        private ServiceLocator _serviceLocator;
        private EntityManager _entityManager;
        
        public override void InstallBindings()
        {
            BindSelf();
            BindServiceLocator();
            BindEntityManager();
            BindEcsStartup();
            
            _sceneContext.PostResolve += InstallServiceLocatorDependencies;
        }

        void IFinishGameListener.OnFinish()
        {
            _sceneContext.PostResolve -= InstallServiceLocatorDependencies;
        }

        private void BindSelf()
        {
            Container.BindInterfacesTo<EcsEngineInstaller>().FromInstance(this).AsSingle();
        }
        
        private void BindServiceLocator()
        {
            Container.Bind<ServiceLocator>().AsCached()
                .OnInstantiated<ServiceLocator>((_, it) => _serviceLocator = it);
        }
        
        private void BindEntityManager()
        {
            Container.BindInterfacesTo<EntityManager>().AsSingle()
                .OnInstantiated<EntityManager>((_, it) => _entityManager = it);
        }
        
        private void BindEcsStartup()
        {
            Container.BindInterfacesTo<EcsStartup>().AsSingle();
        }
        
        private void InstallServiceLocatorDependencies()
        {
            _serviceLocator.Bind(_entityManager);
        }
    }
}