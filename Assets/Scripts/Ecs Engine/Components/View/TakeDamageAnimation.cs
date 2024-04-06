using System;
using UnityEngine;

namespace EcsEngine.Components
{
    [Serializable]
    public struct TakeDamageAnimation
    {
        [SerializeField] 
        private UnityAnimation _unityAnimation;

        public int Value => _unityAnimation.Value;
    }
}