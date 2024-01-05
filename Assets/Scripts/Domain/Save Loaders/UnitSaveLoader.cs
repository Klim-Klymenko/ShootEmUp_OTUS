using System.Collections.Generic;
using GameEngine;
using UnityEngine;

namespace Domain
{
    internal sealed class UnitSaveLoader : SaveLoadMediator<IUnitsProvider, UnitsData>
    {
        private readonly UnitsCatalog _catalog;
        private readonly UnitSpawner _unitSpawner;
        private readonly IUnitDestroyer _unitDestroyer;
        
        internal UnitSaveLoader(IUnitsProvider unitManager, UnitsCatalog catalog, UnitSpawner unitSpawner, IUnitDestroyer unitDestroyer) : base(unitManager)
        {
            _catalog = catalog;
            _unitSpawner = unitSpawner;
            _unitDestroyer = unitDestroyer;
        }
        
        internal override UnitsData ConvertToData(IUnitsProvider unitsProvider)
        {
            List<Unit> unitPrefabs = new();
            List<Vector3> unitsPositions = new();
            List<Quaternion> unitsRotations = new();
            List<int> unitsHitPoints = new();
            
            List<Unit> sceneUnits = new(unitsProvider.GetAllUnits());
            
            for (int i = 0; i < sceneUnits.Count; i++)
            {
                if (_catalog.TryGetPrefab(sceneUnits[i].Type, out Unit unitPrefab))
                    unitPrefabs.Add(unitPrefab);
                
                unitsPositions.Add(sceneUnits[i].Position);
                unitsRotations.Add(Quaternion.Euler(sceneUnits[i].Rotation));
                unitsHitPoints.Add(sceneUnits[i].HitPoints);
            }
            
            return new UnitsData
            {
                
                Units = unitPrefabs,
                UnitsPositions = unitsPositions,
                UnitsRotations = unitsRotations,
                UnitsHitPoints = unitsHitPoints
            };
        }

        internal override void ApplyData(UnitsData data)
        {
            _unitDestroyer.DestroyUnits();
            _unitSpawner.SpawnUnits(data.Units, data.UnitsPositions, data.UnitsRotations, data.UnitsHitPoints);
        }
    }
}