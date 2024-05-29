using System;
using UnityEngine;

namespace Upgrades.Configs
{
    [CreateAssetMenu(fileName = "UpgradeCatalog", menuName = "Configs/Upgrades/UpgradeCatalog")]
    internal sealed class UpgradeCatalog : ScriptableObject
    {
        [SerializeField]
        private UpgradeConfig[] _configs;
        
        internal UpgradeConfig[] GetAllUpgradeConfigs()
        {
            return _configs;
        }

        internal UpgradeConfig FindUpgradeConfig(string id)
        {
            int length = _configs.Length;
            
            for (int i = 0; i < length; i++)
            {
                UpgradeConfig config = _configs[i];
                
                if (config.Id == id)
                    return config;
            }

            throw new Exception($"Config with {id} is not found!");
        }
    }
}