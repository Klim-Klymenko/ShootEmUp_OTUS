using System;
using UnityEngine;

namespace EcsEngine.Components
{
    [Serializable]
    public struct UnityAnimation
    {
        [SerializeField]
        private string _animationName;
        
        private int _animationHash;

        public int Value
        {
            get
            {
                if (_animationHash == 0)
                    _animationHash = Animator.StringToHash(_animationName);

                return _animationHash;
            }
        }
    }
}