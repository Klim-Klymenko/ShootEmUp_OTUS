using System;
using UnityEngine;

namespace EcsEngine.Components.View
{
    [Serializable]
    public struct TakeDamageClip
    {
        [field: SerializeField]
        public AudioClip Value { get; private set; }
    }
}