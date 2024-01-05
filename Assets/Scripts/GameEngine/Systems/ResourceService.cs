using System;
using System.Collections.Generic;
using System.Linq;
using SaveSystem;
using Sirenix.OdinInspector;
using Zenject;

namespace GameEngine
{
    //Нельзя менять!
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
            IEnumerable<Resource> resourcesToProvider = sceneResources.Values;

            foreach (var resource in resourcesToProvider)
            {
                if (resource == null) continue;

                yield return resource;
            }
        }
    }
}