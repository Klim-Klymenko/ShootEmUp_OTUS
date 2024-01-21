using System.Collections.Generic;
using UnityEngine;

namespace GameEngine
{
    [CreateAssetMenu(fileName = "UnitsCatalog", menuName = "Domain/UnitsCatalog")]
    public sealed class UnitsCatalog : ScriptableObject
    {
        [SerializeField] private List<Unit> _unitPrefabs;

        public bool TryGetPrefabs(IReadOnlyList<string> types, out List<Unit> units)
        {
            units = new List<Unit>();

            for (int i = 0; i < types.Count; i++)
            {
                if (TryGetPrefab(types[i], out Unit unit))
                    units.Add(unit);

                else
                {
                    units = null;
                    return false;
                }
            }

            return true;
        }

        public bool TryGetPrefab(string type, out Unit unitPrefab)
        {
            for (int i = 0; i < _unitPrefabs.Count; i++)
            {
                if (_unitPrefabs[i].Type == type)
                {
                    unitPrefab = _unitPrefabs[i];
                    return true;
                }
            }
            
            unitPrefab = null;
            return false;
        }
    }
}