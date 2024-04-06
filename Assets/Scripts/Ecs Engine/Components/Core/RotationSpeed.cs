using System;
using UnityEngine;

namespace EcsEngine.Components
{
    [Serializable]
    public struct RotationSpeed
    {
        [field: SerializeField]
        public float Value { get; private set; }
    }
}