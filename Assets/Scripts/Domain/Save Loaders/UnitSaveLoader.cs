using JetBrains.Annotations;
using SaveSystem;

namespace Domain
{
    [UsedImplicitly]
    internal sealed class UnitSaveLoader : SaveLoadMediator<UnitsFacade, UnitsData>
    {
        internal UnitSaveLoader(UnitsFacade unitsFacade, IGameRepository repository) : base(unitsFacade, repository) { }
        
        protected override UnitsData ConvertToData(UnitsFacade unitsFacade)
        {
            return unitsFacade.GetUnitsData();
        }

        protected override void ApplyData(UnitsFacade unitsFacade, UnitsData data)
        {
            unitsFacade.RespawnUnits(data);
        }
    }
}