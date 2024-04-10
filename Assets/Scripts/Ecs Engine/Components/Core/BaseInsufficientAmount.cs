using System;
using UnityEngine;

namespace EcsEngine.Components
{
    [Serializable]
    public struct BaseInsufficientAmount
    {
        [field: SerializeField]
        public int Value { get; private set; }
    }
}