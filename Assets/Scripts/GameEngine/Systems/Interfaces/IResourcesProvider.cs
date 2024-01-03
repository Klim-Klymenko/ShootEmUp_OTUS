using System.Collections.Generic;
using GameEngine;

namespace SaveSystem
{
    public interface IResourcesProvider
    {
        IEnumerable<Resource> GetResources();
    }
}