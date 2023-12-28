using System;
using SaveSystem;
using Zenject;

namespace Controllers
{
    public sealed class ResourceLoader_ResourcesSpawner_Controller : IInitializable, IDisposable
    {
        private readonly ResourceInstaller _resourceInstaller;
        private readonly ResourcesSaveLoader _resourcesLoader;
        
        public ResourceLoader_ResourcesSpawner_Controller(ResourceInstaller resourceInstaller, ResourcesSaveLoader resourcesLoader)
        {
            _resourceInstaller = resourceInstaller;
            _resourcesLoader = resourcesLoader;
        }


        void IInitializable.Initialize()
        {
            _resourcesLoader.OnDataLoaded += InstallResources;   
        }

        void IDisposable.Dispose()
        {
            _resourcesLoader.OnDataLoaded -= InstallResources;
        }
        
        private void InstallResources(ResourcesData data)
        {
            _resourceInstaller.InstallResources(data);
        }
    }
}