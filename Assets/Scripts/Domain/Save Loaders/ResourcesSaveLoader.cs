using System.Collections.Generic;
using SaveSystem;
using GameEngine;
using JetBrains.Annotations;

namespace Domain
{
    [UsedImplicitly]
    internal sealed class ResourcesSaveLoader : SaveLoadMediator<IResourcesProvider, ResourcesData>
    {
        internal ResourcesSaveLoader(IResourcesProvider service, IGameRepository repository) : base(service, repository) { }

        protected override ResourcesData ConvertToData(IResourcesProvider resourcesProvider)
        {
            List<int> resourcesAmount = new();
            List<string> resourcesIDs = new();

            foreach (Resource resource in resourcesProvider.GetResources())
            {
                resourcesAmount.Add(resource.Amount);
                resourcesIDs.Add(resource.ID);
            }
            
            return new ResourcesData
            {
                ResourcesAmount = resourcesAmount,
                ResourcesIDs = resourcesIDs
            };
        }

        protected override void ApplyData(IResourcesProvider provider, ResourcesData data)
        {
            List<Resource> resources = new(provider.GetResources());
            
            for (int i = 0; i < resources.Count; i++)
            {
                Resource resource = resources[i];
                
                resource.Amount = data.ResourcesAmount[i];
                resource.ID = data.ResourcesIDs[i];
            }   
        }
    }
}