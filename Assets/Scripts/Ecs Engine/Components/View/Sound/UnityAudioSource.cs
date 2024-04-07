using System;
using UnityEngine;

namespace EcsEngine.Components.View
{
    [Serializable]   
    public struct UnityAudioSource
    {
        [field: SerializeField]
        public AudioSource Value { get; private set; }
    }
}