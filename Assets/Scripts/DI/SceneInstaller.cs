using Controllers;
using GameEngine;
using UnityEngine;
using Zenject;

namespace SaveSystem
{
    public sealed class SceneInstaller : MonoInstaller
    {
        [SerializeField]
        private UnitManager _unitManager;
        
        [SerializeField]
        private ResourceService _resourceService;
        
        [SerializeField]
        private SaveLoadManager _saveLoadManager;
        
        public override void InstallBindings()
        {
            BindSceneObjects();
            BindManagers();
            BindDataLayer();
            BindSpawners();
            BindInstallers();
            BindControllers();
            InjectManagers();
        }

        private void BindDataLayer()
        {
            Container.BindInterfacesAndSelfTo<UnitSaveLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<ResourcesSaveLoader>().AsSingle();
            Container.BindInterfacesTo<Repository>().AsSingle();
        }

        private void BindControllers()
        {
            Container.BindInterfacesTo<UnitManager_UnitLoader_Controller>().AsSingle();
            Container.BindInterfacesTo<UnitLoader_UnitSpawner_Controller>().AsSingle();
            Container.BindInterfacesTo<ResourceLoader_ResourcesSpawner_Controller>().AsSingle();
        }

        private void BindSpawners()
        {
            Container.Bind<UnitSpawner>().AsSingle();
        }

        private void BindInstallers()
        {
            Container.Bind<ResourceInstaller>().AsSingle();
        }
        
        private void BindSceneObjects()
        {
            Container.Bind<Unit>().FromComponentsInHierarchy().AsSingle();
            Container.Bind<Resource>().FromComponentsInHierarchy().AsSingle();
        }

        private void BindManagers()
        {
            Container.BindInterfacesAndSelfTo<UnitManager>().FromInstance(_unitManager).AsSingle();
            Container.BindInterfacesTo<ResourceService>().FromInstance(_resourceService).AsSingle();
            Container.Bind<SaveLoadManager>().FromInstance(_saveLoadManager).AsSingle();
        }

        private void InjectManagers()
        {
            Container.Inject(_unitManager);
            Container.Inject(_resourceService);
            Container.Inject(_saveLoadManager);
        }
    }
}