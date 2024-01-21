using SaveSystem;
using UnityEngine;
using Zenject;

namespace GameEngine
{
    internal sealed class GameEngineInstaller : MonoInstaller
    {
        [SerializeField]
        private UnitsManager _unitsManager;
        
        [SerializeField]
        private ResourceService _resourceService;

        [SerializeField]
        private UnitsCatalog _unitsCatalog;
        
        public override void InstallBindings()
        {
            BindObjects();
            BindManagers();
            BindUnitsCatalog();
        }

        private void BindObjects()
        {
            Container.Bind<Unit>().FromComponentsInHierarchy().AsCached();
            Container.Bind<Resource>().FromComponentsInHierarchy().AsCached();
        }
        
        private void BindManagers()
        {
            Container.Bind<UnitsManager>().FromMethod(InjectUnitsManager).AsSingle();
            Container.Bind<IResourcesProvider>().FromMethod(InjectResourceServiceProvider).AsCached();
        }

        private void BindUnitsCatalog()
        {
            Container.Bind<UnitsCatalog>().FromInstance(_unitsCatalog).AsSingle();
        }
        
        private UnitsManager InjectUnitsManager()
        {
            Container.Inject(_unitsManager);
            return _unitsManager;
        }
        
        private IResourcesProvider InjectResourceServiceProvider()
        {
            Container.Inject(_resourceService);
            return _resourceService;
        }
    }
}