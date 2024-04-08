using Common;
using GameCycle;
using UnityEngine;
using Zenject;

namespace EcsEngine.Extensions
{
    internal sealed class EcsEngineInstaller : MonoInstaller, IFinishGameListener
    {
        [SerializeField] 
        private SceneContext _sceneContext;
        
        private ServiceLocator _serviceLocator;
        private EntityManager _entityManager;
        
        public override void InstallBindings()
        {
            BindServiceLocator();
            BindEntityManager();
            BindEcsStartup();
            
            _sceneContext.PostResolve += InstallServiceLocatorDependencies;
        }

        void IFinishGameListener.OnFinish()
        {
            _sceneContext.PostResolve -= InstallServiceLocatorDependencies;
        }
        
        private void BindServiceLocator()
        {
            Container.Bind<ServiceLocator>().AsCached()
                .OnInstantiated<ServiceLocator>((_, it) => _serviceLocator = it);
        }
        
        private void BindEntityManager()
        {
            Container.Bind<EntityManager>().AsSingle()
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