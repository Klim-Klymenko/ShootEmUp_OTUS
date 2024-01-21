using JetBrains.Annotations;
using SaveSystem;

namespace Domain
{
    [UsedImplicitly]
    internal sealed class UnitSaveLoader : SaveLoadMediator<UnitsData>
    {
        private readonly UnitsFacade _unitsFacade;

        internal UnitSaveLoader(UnitsFacade unitsFacade, IGameRepository repository) : base(repository)
        {
            _unitsFacade = unitsFacade;
        }
        
        protected override UnitsData ConvertToData()
        {
            return _unitsFacade.GetUnitsData();
        }

        protected override void ApplyData(UnitsData data)
        {
            _unitsFacade.RespawnUnits(data);
        }
    }
}