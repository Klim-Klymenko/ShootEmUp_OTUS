using System.Linq;
using GameEngine;

namespace SaveSystem
{
    public sealed class ResourcesSaveLoader : SaveLoadMediator<IResourcesProvider, ResourcesData>
    {
        public ResourcesSaveLoader(IResourcesProvider service) : base(service) { }

        protected override ResourcesData ConvertToData(IResourcesProvider resourcesProvider)
        {
            return new ResourcesData
            {
                Resources = new(resourcesProvider.GetResources()),
                ResourcesAmounts = resourcesProvider.GetResources().Select(resource => resource.Amount).ToList(),
                ResourcesIDs = resourcesProvider.GetResources().Select(resource => resource.ID).ToList()
            };
        }
    }
}