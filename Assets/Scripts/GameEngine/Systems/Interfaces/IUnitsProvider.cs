using System.Collections.Generic;

namespace GameEngine
{
    public interface IUnitsProvider
    {
        IEnumerable<Unit> GetAllUnits();
    }
}