using System;
using UnityEngine;

namespace EcsEngine.Components.View
{
    [Serializable]
    public struct AttackClip
    {
        [field: SerializeField]
        public AudioClip Value { get; private set; }
    }
}