using System;
using System.Collections.Generic;
using System.Linq;
using SaveSystem;
using Sirenix.OdinInspector;
using Zenject;

namespace GameEngine
{
    [Serializable]
    internal sealed class ResourceService : IResourcesProvider
    {
        [ShowInInspector, ReadOnly]
        private Dictionary<string, Resource> sceneResources = new();

        [Inject]
        internal void SetResources(IEnumerable<Resource> resources)
        {
            sceneResources = resources.ToDictionary(it => it.ID);
        }

        IEnumerable<Resource> IResourcesProvider.GetResources()
        {
            IEnumerable<Resource> resourcesToProvide = sceneResources.Values;

            foreach (Resource resource in resourcesToProvide)
            {
                if (resource == null) continue;

                yield return resource;
            }
        }
    }
}