using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public abstract class Context : MonoBehaviour
    {
        [SerializeField]
        private protected Installer[] Installers;

        private static readonly ServiceLocator _sceneServiceLocator;
        private ServiceLocator _childServiceLocator;
        private ServiceLocatorInstaller _serviceLocatorInstaller;
        
        private protected DiContainer DiContainer;
        private readonly Injector _injector = new();
        
        static Context()
        {
            _sceneServiceLocator = new ServiceLocator();
        }

        private protected void InitializeDi(ServiceLocator childServiceLocator = null)
        {
            _childServiceLocator = childServiceLocator;
            
            DiContainer = childServiceLocator == null ?
                new DiContainer(_sceneServiceLocator, _injector) : new DiContainer(childServiceLocator, _injector);

            _serviceLocatorInstaller = childServiceLocator == null ?
                new ServiceLocatorInstaller(_sceneServiceLocator) : new ServiceLocatorInstaller(childServiceLocator);
        }
        
        private protected void InstallServices(SystemInstallableArgs args) => 
            _serviceLocatorInstaller.InstallServices(args, Installers);
        
        private protected void InstallServices() =>
            _serviceLocatorInstaller.InstallServices(DiContainer, Installers);

        private protected void InjectSceneContext()
        {
            for (int i = 0; i < Installers.Length; i++)
            {
                IEnumerable<object> injectables = Installers[i].ProvideInjectables();
                
                foreach (object injectable in injectables)
                    _injector.Inject(injectable, _sceneServiceLocator);
            }
            
            MonoBehaviour[] sceneComponents = FindObjectsOfType<MonoBehaviour>(true);

            for (int i = 0; i < sceneComponents.Length; i++)
                _injector.Inject(sceneComponents[i], _sceneServiceLocator);
        }

        private protected void InjectGameObjectContext()
        {
            for (int i = 0; i < Installers.Length; i++)
            {
                IEnumerable<object> injectables = Installers[i].ProvideInjectables();

                foreach (object injectable in injectables)
                    _injector.Inject(injectable, _childServiceLocator, _sceneServiceLocator);
            }
        }
    }
}