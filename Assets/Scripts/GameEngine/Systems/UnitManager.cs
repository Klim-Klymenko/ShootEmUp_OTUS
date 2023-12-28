using System;
using System.Collections.Generic;
using SaveSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace GameEngine
{
    //Нельзя менять!
    [Serializable]
    public sealed class UnitManager : IUnitsProvider, IUnitSpawner
    {
        [SerializeField]
        private Transform container;

        [ShowInInspector, ReadOnly]
        private HashSet<Unit> sceneUnits = new();

        public UnitManager() { }

        public UnitManager(Transform container) => this.container = container;

        [Inject]
        public void SetupUnits(IEnumerable<Unit> units) => sceneUnits = new HashSet<Unit>(units);

        public void SetContainer(Transform container) => this.container = container;

        [Button]
        public Unit SpawnUnit(Unit prefab, Vector3 position, Quaternion rotation)
        {
            var unit = Object.Instantiate(prefab, position, rotation, container);
            unit.gameObject.SetActive(true);
            sceneUnits.Add(unit);
            return unit;
        }

        [Button]
        public void DestroyUnit(Unit unit)
        {
            if (sceneUnits.Remove(unit))
            {
                Object.Destroy(unit.gameObject);
            }
        }

        IEnumerable<Unit> IUnitsProvider.GetAllUnits()
        {
            foreach (var unit in sceneUnits)
            {
                if (unit == null) continue;
                
                yield return unit;
            }
        }

        public void ClearPreviousUnits()
        {
            //if pool would exist, we would return units to the pool
            foreach (var unit in sceneUnits)
                unit.gameObject.SetActive(false);
            
            sceneUnits.Clear();
        }
    }
}