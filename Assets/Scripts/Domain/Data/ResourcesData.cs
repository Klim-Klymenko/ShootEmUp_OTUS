using System.Collections.Generic;

namespace Domain
{
    [System.Serializable]
    internal struct ResourcesData
    {
        public List<int> ResourcesAmount;
        public List<string> ResourcesIDs;
    }
}