using System;
using UnityEngine;

namespace EcsEngine.Components
{
    [Serializable]
    public struct Health
    {
        [field: SerializeField]
        public int MinHitPoints { get; private set; }
        
        public int HitPoints;
    }
}