using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Upgrades.Tables
{
    [Serializable]
    public sealed class StatUpgradeTable
    {
        [SerializeField]
        private int _startStatValue;

        [SerializeField] 
        private int _endStatValue;
        
        [ReadOnly]
        [SerializeField]
        private int _statValueStep;
        
        [ReadOnly]
        [SerializeField]
        private int[] _statValues;

        public void OnValidate(int maxLevel)
        {
            EvaluateStatValues(maxLevel);
        }

        public int GetStatValue(int level)
        {
            return _statValues[level - 1];
        }

        private void EvaluateStatValues(int maxLevel)
        {
            _statValues = new int[maxLevel];
    
            int statValuesRange = _endStatValue - _startStatValue;
            _statValueStep = statValuesRange / maxLevel;

            for (int i = 0; i < maxLevel; i++)
            {
                float previousStatValue = i == 0 ? _startStatValue : _statValues[i - 1];
                int statValue = (int) Mathf.MoveTowards(previousStatValue, _endStatValue, _statValueStep);
        
                _statValues[i] = statValue;
            }
        }
    }
}