using System;
using EcsEngine.Data;

namespace EcsEngine.Components
{
    [Serializable]
    public struct UnitState
    {
        public State Value;
        public int Hash => Value.GetHashCode();
    }
}