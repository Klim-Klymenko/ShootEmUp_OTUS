using System.Collections.Generic;
using System.Linq;
using SaveSystem;
using GameEngine;

namespace Domain
{
    internal sealed class ResourcesSaveLoader : SaveLoadMediator<IResourcesProvider, ResourcesData>
    {
        private readonly ResourceInstaller _resourceInstaller;
        
        public ResourcesSaveLoader(IResourcesProvider service, ResourceInstaller resourceInstaller) : base(service)
        {
            _resourceInstaller = resourceInstaller;
        }

        internal override ResourcesData ConvertToData(IResourcesProvider resourcesProvider)
        {
            List<int> resourcesAmount = new();
            List<string> resourcesIDs = new();
            
            List<Resource> resources = new(resourcesProvider.GetResources());

            for (int i = 0; i < resources.Count; i++)
            {
                resourcesAmount.Add(resources[i].Amount);
                resourcesIDs.Add(resources[i].ID);
            }
            
            return new ResourcesData
            {
                ResourcesAmount = resourcesAmount,
                ResourcesIDs = resourcesIDs
            };
        }

        internal override void ApplyData(ResourcesData data)
        {
            _resourceInstaller.InstallResources(data.ResourcesAmount, data.ResourcesIDs);
        }
    }
}