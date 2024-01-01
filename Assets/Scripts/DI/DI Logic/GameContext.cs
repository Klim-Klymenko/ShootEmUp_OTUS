using UnityEngine;

namespace ShootEmUp
{
    public abstract class GameContext : MonoBehaviour
    {
        [SerializeField]
        protected Installer[] Installers;
        
        private static readonly ServiceLocator _sceneServiceLocator;
        private ServiceLocatorInstaller _serviceLocatorInstaller;
        
        protected DiContainer DiContainer;
        private readonly Injector _injector = new();
        
        static GameContext()
        {
            _sceneServiceLocator = new ServiceLocator();
        }

        protected void InitializeDi(ServiceLocator childServiceLocator = null)
        {
            DiContainer = childServiceLocator == null ?
                new DiContainer(_sceneServiceLocator, _injector) : new DiContainer(childServiceLocator, _injector);

            _serviceLocatorInstaller = childServiceLocator == null ?
                new ServiceLocatorInstaller(_sceneServiceLocator) : new ServiceLocatorInstaller(childServiceLocator);
        }
        
        protected void InstallServices(SystemInstallablesArgs args) => 
            _serviceLocatorInstaller.InstallServices(args, Installers);
        
        protected void InstallServices() =>
            _serviceLocatorInstaller.InstallServices(DiContainer, Installers);
        
        protected void InjectSceneContext() => DiContainer.Inject(Installers);
        
        protected void InjectGameObjectContext() =>
            DiContainer.Inject(Installers, _sceneServiceLocator);
    }
}