using System;
using EcsEngine.Extensions;
using Leopotam.EcsLite;
using UnityEngine;

namespace EcsEngine.Components
{
    [Serializable]
    public struct Spawn
    {
        public Entity Prefab;
        public Transform SpawnPoint;
    }
}