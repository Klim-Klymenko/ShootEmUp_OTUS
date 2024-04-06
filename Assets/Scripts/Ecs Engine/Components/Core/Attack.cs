using System;
using UnityEngine;

namespace EcsEngine.Components
{
    [Serializable]
    public struct Attack
    {
        [field: SerializeField]
        public float Interval { get; private set; }
        
        [field: SerializeField]
        public float Range { get; private set; }
    }
}