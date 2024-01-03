using System.Linq;
using SaveSystem;
using GameEngine;

namespace Domain
{
    public sealed class ResourcesSaveLoader : SaveLoadMediator<IResourcesProvider, ResourcesData>
    {
        private readonly ResourceInstaller _resourceInstaller;
        
        public ResourcesSaveLoader(IResourcesProvider service, ResourceInstaller resourceInstaller) : base(service)
        {
            _resourceInstaller = resourceInstaller;
        }

        protected override ResourcesData ConvertToData(IResourcesProvider resourcesProvider)
        {
            return new ResourcesData
            {
                ResourcesAmount = resourcesProvider.GetResources().Select(resource => resource.Amount).ToList(),
                ResourcesIDs = resourcesProvider.GetResources().Select(resource => resource.ID).ToList()
            };
        }

        protected override void ApplyData(ResourcesData data)
        {
            _resourceInstaller.InstallResources(data.ResourcesAmount, data.ResourcesIDs);
        }
    }
}