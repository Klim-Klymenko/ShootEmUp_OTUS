using System;
using EcsEngine.Data;

namespace EcsEngine.Components
{
    [Serializable]
    public struct MoveState
    {
        public State Value;
        public int Hash => Value.GetHashCode();
    }
}