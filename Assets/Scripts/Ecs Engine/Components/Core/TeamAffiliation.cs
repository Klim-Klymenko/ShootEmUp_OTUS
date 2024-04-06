using System;
using EcsEngine.Data;
using UnityEngine;

namespace EcsEngine.Components
{
    [Serializable]
    public struct TeamAffiliation
    {
        [field: SerializeField]
        public Team Value { get; private set; }
    }
}