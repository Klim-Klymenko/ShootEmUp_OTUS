using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sample
{
    [Serializable]
    internal sealed class PriceTable
    {
        [Space]
        [SerializeField]
        private int _basePrice;

        [Space]
        [ListDrawerSettings(OnBeginListElementGUI = "DrawLevels")]
        [SerializeField]
        private int[] _levels;

        internal int GetPrice(int level)
        {
            int index = level - 1;
            index = Mathf.Clamp(index, 0, _levels.Length - 1);
                
            return _levels[index];
        }

        private void DrawLevels(int index)
        {
            GUILayout.Space(8);
            GUILayout.Label($"Level #{index + 1}");
        }
        
        internal void OnValidate(int maxLevel)
        {
            EvaluatePriceTable(maxLevel);
        }

        private void EvaluatePriceTable(int maxLevel)
        {
            int[] table = new int[maxLevel];
            table[0] = new int();
                
            for (int level = 2; level <= maxLevel; level++)
            {
                int price = _basePrice * level;
                table[level - 1] = price;
            }

            _levels = table;
        }
    }
}