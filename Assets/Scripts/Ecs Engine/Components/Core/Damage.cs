using System;
using UnityEngine;

namespace EcsEngine.Components
{
    [Serializable]
    public struct Damage
    {
        [field: SerializeField]
        public int Value { get; private set; }
    }
}