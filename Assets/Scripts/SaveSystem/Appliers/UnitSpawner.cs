using System.Collections.Generic;
using SaveSystem;

namespace GameEngine
{
    public sealed class UnitSpawner
    {
        private readonly IUnitSpawner _unitSpawner;
        
        public UnitSpawner(IUnitSpawner unitSpawner)
        {
            _unitSpawner = unitSpawner;
        }
        
        public void SpawnUnits(UnitsData data)
        {
            List<Unit> units = data.Units;
            for (int i = 0; i < units.Count; i++)
            {
                Unit unit = _unitSpawner.SpawnUnit(units[i], data.UnitsPositions[i], data.UnitsRotations[i]);
                unit.HitPoints = data.UnitsHitPoints[i];
                unit.Type = data.UnitsTypes[i];
            }
        }
    }
}