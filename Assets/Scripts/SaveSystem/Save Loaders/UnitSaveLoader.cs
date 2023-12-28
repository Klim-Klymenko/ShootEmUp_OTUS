using System.Linq;
using UnityEngine;

namespace SaveSystem
{
    public sealed class UnitSaveLoader : SaveLoadMediator<IUnitsProvider, UnitsData>
    {
        public UnitSaveLoader(IUnitsProvider unitManager) : base(unitManager) { }
        
        protected override UnitsData ConvertToData(IUnitsProvider unitsProvider)
        {
            return new UnitsData
            {
                Units = new(unitsProvider.GetAllUnits()),
                UnitsPositions = unitsProvider.GetAllUnits().Select(unit => unit.Position).ToList(),
                UnitsRotations = unitsProvider.GetAllUnits().Select(unit => Quaternion.Euler(unit.Rotation)).ToList(),
                UnitsHitPoints = unitsProvider.GetAllUnits().Select(unit => unit.HitPoints).ToList(),
                UnitsTypes = unitsProvider.GetAllUnits().Select(unit => unit.Type).ToList()
            };
        }
    }
}