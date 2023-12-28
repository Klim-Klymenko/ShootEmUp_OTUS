using System.Collections.Generic;
using GameEngine;

namespace SaveSystem
{
    [System.Serializable]
    public struct ResourcesData
    {
        public List<Resource> Resources;
        public List<int> ResourcesAmounts;
        public List<string> ResourcesIDs;
    }
}