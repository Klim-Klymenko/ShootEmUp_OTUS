using System;
using GameEngine;
using SaveSystem;
using UnityEngine;
using Zenject;

namespace Controllers
{
    public sealed class UnitLoader_UnitSpawner_Controller : IInitializable, IDisposable
    {
        private readonly UnitSaveLoader _unitLoader;
        private readonly UnitSpawner _unitSpawner;
        
        public UnitLoader_UnitSpawner_Controller(UnitSaveLoader unitLoader, UnitSpawner unitSpawner)
        {
            _unitLoader = unitLoader;
            _unitSpawner = unitSpawner;
        }
        
        void IInitializable.Initialize()
        {
            _unitLoader.OnDataLoaded += SpawnUnits;
        }
        
        void IDisposable.Dispose()
        {
            _unitLoader.OnDataLoaded -= SpawnUnits;
        }
        
        private void SpawnUnits(UnitsData unitsData)
        {
            _unitSpawner.SpawnUnits(unitsData);
        }
    }
}