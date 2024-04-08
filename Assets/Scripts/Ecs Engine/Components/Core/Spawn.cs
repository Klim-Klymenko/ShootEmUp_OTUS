using System;
using EcsEngine.Extensions;
using Leopotam.EcsLite;
using UnityEngine;

namespace EcsEngine.Components
{
    [Serializable]
    public struct Spawn
    {
        [field: SerializeField]
        public Entity Prefab { get; private set; }
        
        [field: SerializeField]
        public Transform SpawnPoint { get; private set; }
    }
}