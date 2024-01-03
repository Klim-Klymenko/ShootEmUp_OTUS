using System.Collections.Generic;

namespace Domain
{
    [System.Serializable]
    public struct ResourcesData
    {
        public List<int> ResourcesAmount;
        public List<string> ResourcesIDs;
    }
}