using System;
using System.Collections.Generic;
using UnityEngine;
using GameEngine;
using JetBrains.Annotations;

namespace Domain
{
    [UsedImplicitly]
    internal sealed class UnitsFacade
    {
        private readonly UnitsCatalog _unitsCatalog;
        private readonly UnitsManager _unitsManager;
        
        internal UnitsFacade(UnitsCatalog unitsCatalog, UnitsManager unitsManager)
        {
            _unitsCatalog = unitsCatalog;
            _unitsManager = unitsManager;
        }
        
        internal void RespawnUnits(UnitsData data)
        {
            _unitsManager.DestroyUnits();
            SpawnUnits(data);
        }
        
        internal UnitsData GetUnitsData()
        {
            List<string> types = new();
            List<Vector3> positions = new();
            List<Quaternion> rotations = new();
            List<int> hitPoints = new();

            foreach (Unit unit in _unitsManager.GetAllUnits())
            {
                types.Add(unit.Type);
                positions.Add(unit.Position);
                rotations.Add(Quaternion.Euler(unit.Rotation));
                hitPoints.Add(unit.HitPoints);
            }
            
            return new UnitsData
            {
                Types = types,
                Positions = positions,
                Rotations = rotations,
                HitPoints = hitPoints
            };
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