using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace GameEngine
{
    //Нельзя менять!
    [Serializable]
    internal sealed class UnitManager : IUnitsProvider, IUnitSpawner, IUnitDestroyer
    {
        [SerializeField]
        private Transform container;

        [ShowInInspector, ReadOnly]
        private HashSet<Unit> sceneUnits = new();

        internal UnitManager() { }

        internal UnitManager(Transform container) => this.container = container;

        [Inject]
        internal void SetupUnits(IEnumerable<Unit> units) => sceneUnits = new HashSet<Unit>(units);

        internal void SetContainer(Transform container) => this.container = container;

        [Button]
        public Unit SpawnUnit(Unit prefab, Vector3 position, Quaternion rotation)
        {
            var unit = Object.Instantiate(prefab, position, rotation, container);
            unit.gameObject.SetActive(true);
            sceneUnits.Add(unit);
            return unit;
        }

        [Button]
        internal void DestroyUnit(Unit unit)
        {
            if (sceneUnits.Remove(unit))
            {
                Object.Destroy(unit.gameObject);
            }
        }

        void IUnitDestroyer.DestroyUnits()
        {
            foreach (var unit in sceneUnits)
                Object.Destroy(unit.gameObject);
            
            sceneUnits.Clear();
        }
        
        IEnumerable<Unit> IUnitsProvider.GetAllUnits()
        {
            foreach (var unit in sceneUnits)
            {
                if (unit == null) continue;
                
                yield return unit;
            }
        }
    }
}