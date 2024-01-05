using System.Collections.Generic;
using GameEngine;
using UnityEngine;

namespace Domain
{
    [System.Serializable]
    internal struct UnitsData
    {
        public List<Unit> Units;
        public List<Vector3> UnitsPositions;
        public List<Quaternion> UnitsRotations;
        public List<int> UnitsHitPoints;
    }
}