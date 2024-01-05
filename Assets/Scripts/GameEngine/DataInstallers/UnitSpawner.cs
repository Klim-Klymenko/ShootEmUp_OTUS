using System.Collections.Generic;
using UnityEngine;

namespace GameEngine
{
    public sealed class UnitSpawner
    {
        private readonly IUnitSpawner _unitSpawner;
        
        public UnitSpawner(IUnitSpawner unitSpawner)
        {
            _unitSpawner = unitSpawner;
        }
        
        public void SpawnUnits(List<Unit> units, List<Vector3> positions, List<Quaternion> rotations, List<int> hitPoints)
        {
            for (int i = 0; i < units.Count; i++)
            {
                Unit unit = _unitSpawner.SpawnUnit(units[i], positions[i], rotations[i]);
                unit.HitPoints = hitPoints[i];
            }
        }
    }
}