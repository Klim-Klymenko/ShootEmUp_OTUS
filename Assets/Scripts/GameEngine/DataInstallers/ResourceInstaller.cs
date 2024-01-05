using System.Collections.Generic;
using SaveSystem;

namespace GameEngine
{
    public sealed class ResourceInstaller
    {
        private readonly IResourcesProvider _resourcesProvider;
        
        internal ResourceInstaller(IResourcesProvider resourcesProvider)
        {
            _resourcesProvider = resourcesProvider;
        }
        
        public void InstallResources(List<int> amount, List<string> ids)
        {
            List<Resource> resources = new(_resourcesProvider.GetResources());
            
            for (int i = 0; i < resources.Count; i++)
            {
                resources[i].Amount = amount[i];
                resources[i].ID = ids[i];
            }       
        }
    }
}