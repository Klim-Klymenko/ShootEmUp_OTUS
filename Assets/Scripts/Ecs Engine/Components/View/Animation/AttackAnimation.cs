using System;
using UnityEngine;

namespace EcsEngine.Components
{
    [Serializable]
    public struct AttackAnimation
    {
        [SerializeField]
        private UnityAnimation _unityAnimation;
        
        public int Value => _unityAnimation.Value;
    }
}