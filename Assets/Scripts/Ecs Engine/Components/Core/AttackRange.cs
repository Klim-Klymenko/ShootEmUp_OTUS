using System;
using UnityEngine;

namespace EcsEngine.Components
{
    [Serializable]
    public struct AttackRange
    {
        [field: SerializeField]
        public float Value { get; private set; }
    }
}