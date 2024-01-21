using GameEngine;
using JetBrains.Annotations;

namespace Domain
{
    [UsedImplicitly]
    internal sealed class UnitsFacade
    {
        private readonly UnitsSpawner _unitsSpawner;
        private readonly UnitsDataConverter _unitsDataConverter;
        private readonly UnitsManager _unitsManager;
        
        internal UnitsFacade(UnitsSpawner unitsSpawner, UnitsDataConverter unitsDataConverter, UnitsManager unitsManager)
        {
            _unitsSpawner = unitsSpawner;
            _unitsDataConverter = unitsDataConverter;
            _unitsManager = unitsManager;
        }
        
        internal UnitsData GetUnitsData()
        {
            return _unitsDataConverter.GetUnitsData(_unitsManager.GetAllUnits());
        }
        
        internal void RespawnUnits(UnitsData data)
        {
            _unitsManager.DestroyUnits();
            _unitsSpawner.SpawnUnits(data);
        }
    }
}