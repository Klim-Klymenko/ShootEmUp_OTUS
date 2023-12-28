using System.Collections.Generic;
using System.Collections.ObjectModel;
using GameEngine;

namespace SaveSystem
{
    public interface IResourcesProvider
    {
        IEnumerable<Resource> GetResources();
    }
}