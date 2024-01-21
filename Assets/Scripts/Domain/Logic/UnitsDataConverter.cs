using System.Collections.Generic;
using UnityEngine;
using GameEngine;
using JetBrains.Annotations;

namespace Domain
{
    [UsedImplicitly]
    internal sealed class UnitsDataConverter
    {
        internal UnitsData GetUnitsData(IEnumerable<Unit> units)
        {
            List<string> types = new();
            List<Vector3> positions = new();
            List<Quaternion> rotations = new();
            List<int> hitPoints = new();

            foreach (Unit unit in units)
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
    }
}