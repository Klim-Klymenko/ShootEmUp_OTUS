using System;
using System.Collections.Generic;
using GameEngine;
using JetBrains.Annotations;

namespace Domain
{
    [UsedImplicitly]
    internal sealed class UnitsSpawner
    {
        private readonly UnitsManager _unitsManager;
        private readonly UnitsCatalog _unitsCatalog;
        
        internal UnitsSpawner(UnitsManager unitsManager, UnitsCatalog unitsCatalog)
        {
            _unitsManager = unitsManager;
            _unitsCatalog = unitsCatalog;
        }
        
        internal void SpawnUnits(UnitsData data)
        {
            if (!_unitsCatalog.TryGetPrefabs(data.Types, out List<Unit> unitPrefabs)) 
                throw new NullReferenceException("Can't find unit prefab");
            
            for (int i = 0; i < data.Count; i++)
            {
                Unit unit = _unitsManager.SpawnUnit(unitPrefabs[i], data.Positions[i], data.Rotations[i]);
                unit.HitPoints = data.HitPoints[i];
            }
        }
    }
}