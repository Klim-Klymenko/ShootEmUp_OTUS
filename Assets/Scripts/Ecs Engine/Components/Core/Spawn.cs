using System;
using EcsEngine.Extensions;
using UnityEngine;

namespace EcsEngine.Components
{
    [Serializable]
    public struct Spawn
    {
        [field: SerializeField]
        public Entity Prefab { get; private set; }
        
        [field: SerializeField]
        public Transform FirePoint { get; private set; }
        
        [field: SerializeField]
        public Transform Parent { get; private set; }
    }
}