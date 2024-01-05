using System.ComponentModel;
using UnityEngine;
using Zenject;

namespace GameEngine
{
    internal sealed class GameEngineInstaller : MonoInstaller
    {
        [SerializeField]
        private UnitManager _unitManager;
        
        [SerializeField]
        private ResourceService _resourceService;
        
        [SerializeField]
        private SceneContext _sceneContext;
        
        private void Construct()
        {
            Container.Inject(_unitManager);
            Container.Inject(_resourceService);
        }
        
        public override void InstallBindings()
        {
            BindSavables();
            BindManagers();
            BindDataAppliers();
            
            SubscribeConstruct();
        }
        
        private void OnDestroy()
        {
            UnsubscribeConstruct();
        }

        private void BindSavables()
        {
            Container.Bind<Unit>().FromComponentsInHierarchy().AsCached();
            Container.Bind<Resource>().FromComponentsInHierarchy().AsCached();
        }

        private void BindDataAppliers()
        {
            Container.Bind<UnitSpawner>().AsSingle();
            Container.Bind<ResourceInstaller>().AsSingle();
        }
        
        private void BindManagers()
        {
            Container.BindInterfacesTo<UnitManager>().FromInstance(_unitManager).AsCached();
            Container.BindInterfacesTo<ResourceService>().FromInstance(_resourceService).AsCached();
        }

        private void SubscribeConstruct() => _sceneContext.PostInstall += Construct;
        private void UnsubscribeConstruct() => _sceneContext.PostInstall -= Construct;
    }
}