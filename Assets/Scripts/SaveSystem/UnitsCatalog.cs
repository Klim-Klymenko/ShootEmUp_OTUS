using System.Collections.Generic;
using GameEngine;
using UnityEngine;

namespace Domain
{
    [CreateAssetMenu(fileName = "UnitsCatalog", menuName = "GameEngine/UnitsCatalog")]
    public sealed class UnitsCatalog : ScriptableObject
    {
        [SerializeField]
        private List<Unit> _unitPrefabs;

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