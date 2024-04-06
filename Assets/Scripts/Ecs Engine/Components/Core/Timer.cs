using System;
using UnityEngine;

namespace EcsEngine.Components
{
    [Serializable]
    public struct Timer
    {
        [field: SerializeField]
        public float EndTime { get; private set; }
        
        [field: SerializeField]
        public float Duration { get; private set; }

        public float CurrentTime;
    }
}