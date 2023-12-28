using System.Collections.Generic;
using GameEngine;

namespace SaveSystem
{
    public interface IUnitsProvider
    {
        IEnumerable<Unit> GetAllUnits();
    }
}