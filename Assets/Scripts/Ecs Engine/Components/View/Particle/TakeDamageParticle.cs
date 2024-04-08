using System;
using UnityEngine;

namespace EcsEngine.Components.View
{
    [Serializable]
    public struct TakeDamageParticle
    {
        [field: SerializeField]
        public ParticleSystem Value { get; private set; }
    }
}