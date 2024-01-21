using System;
using System.Collections.Generic;
using UnityEngine;

namespace Domain
{
    [Serializable]
    internal struct UnitsData
    {
        [HideInInspector]
        public List<string> Types;
        
        [HideInInspector]
        public List<Vector3> Positions;
        
        [HideInInspector]
        public List<Quaternion> Rotations;
        
        [HideInInspector]
        public List<int> HitPoints;
        
        public int Count => Types.Count;
    }
}