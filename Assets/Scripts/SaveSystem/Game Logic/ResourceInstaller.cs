using System.Collections.Generic;
using GameEngine;

namespace SaveSystem
{
    public sealed class ResourceInstaller
    {
        private readonly IResourcesProvider _resourcesProvider;
        
        public ResourceInstaller(IResourcesProvider resourcesProvider)
        {
            _resourcesProvider = resourcesProvider;
        }
        
        public void InstallResources(ResourcesData data)
        {
            List<Resource> resources = new(_resourcesProvider.GetResources());
            
            for (int i = 0; i < resources.Count; i++)
            {
                resources[i].Amount = data.ResourcesAmounts[i];
                resources[i].ID = data.ResourcesIDs[i];
            }       
        }
    }
}