using System;
using GameEngine;
using Zenject;
using SaveSystem;

namespace Controllers
{
    public sealed class UnitManager_UnitLoader_Controller : IInitializable, IDisposable
    {
        private readonly UnitManager _unitManager;
        private readonly UnitSaveLoader _unitLoader;
        
        public UnitManager_UnitLoader_Controller(UnitManager unitManager, UnitSaveLoader unitLoader)
        {
            _unitManager = unitManager;
            _unitLoader = unitLoader;
        }
        
        void IInitializable.Initialize()
        {
            _unitLoader.OnDataLoaded += ClearPreviousUnits;
        }

        void IDisposable.Dispose()
        {
            _unitLoader.OnDataLoaded -= ClearPreviousUnits;
        }

        private void ClearPreviousUnits(UnitsData _)
        {
            _unitManager.ClearPreviousUnits();
        }
    }
}