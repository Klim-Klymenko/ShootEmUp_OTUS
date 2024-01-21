using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace GameEngine
{
    [Serializable]
    public sealed class UnitsManager
    {
        [SerializeField]
        private Transform container;

        [ShowInInspector, ReadOnly]
        private HashSet<Unit> sceneUnits = new();

        [Inject]
        public void SetupUnits(IEnumerable<Unit> units)
        {
            sceneUnits = new HashSet<Unit>(units);
        }

        [Button]
        public Unit SpawnUnit(Unit prefab, Vector3 position, Quaternion rotation)
        {
            Unit unit = Object.Instantiate(prefab, position, rotation, container);
            
            unit.gameObject.SetActive(true);
            sceneUnits.Add(unit);
            
            return unit;
        }

        [Button]
        public void DestroyUnit(Unit unit)
        {
            if (sceneUnits.Remove(unit))
                Object.Destroy(unit.gameObject);
        }

        public void DestroyUnits()
        {
            foreach (var unit in sceneUnits)
                Object.Destroy(unit.gameObject);
            
            sceneUnits.Clear();
        }
        
        public IEnumerable<Unit> GetAllUnits()
        {
            foreach (var unit in sceneUnits)
            {
                if (unit == null) continue;
                
                yield return unit;
            }
        }
    }
}