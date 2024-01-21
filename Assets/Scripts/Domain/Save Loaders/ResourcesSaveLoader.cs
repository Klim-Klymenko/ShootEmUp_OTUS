using System.Collections.Generic;
using SaveSystem;
using GameEngine;
using JetBrains.Annotations;

namespace Domain
{
    [UsedImplicitly]
    internal sealed class ResourcesSaveLoader : SaveLoadMediator<ResourcesData>
    {
        private readonly IResourcesProvider _resourcesProvider;

        internal ResourcesSaveLoader(IResourcesProvider resourcesProvider, IGameRepository repository) : base(repository)
        {
            _resourcesProvider = resourcesProvider;
        }

        protected override ResourcesData ConvertToData()
        {
            List<int> resourcesAmount = new();
            List<string> resourcesIDs = new();

            foreach (Resource resource in _resourcesProvider.GetResources())
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

        protected override void ApplyData(ResourcesData data)
        {
            List<Resource> resources = new(_resourcesProvider.GetResources());
            
            for (int i = 0; i < resources.Count; i++)
            {
                Resource resource = resources[i];
                
                resource.Amount = data.ResourcesAmount[i];
                resource.ID = data.ResourcesIDs[i];
            }   
        }
    }
}