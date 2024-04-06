using System;
using UnityEngine;

namespace EcsEngine.Components
{
    [Serializable]
    public struct UnityTransform
    {
        public Transform Value;
    }
}