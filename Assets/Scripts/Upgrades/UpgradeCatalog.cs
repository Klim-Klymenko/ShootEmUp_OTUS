using System;
using UnityEngine;

namespace Sample
{
    [CreateAssetMenu(fileName = "UpgradeCatalog", menuName = "Configs/Upgrades/UpgradeCatalog")]
    public sealed class UpgradeCatalog : ScriptableObject
    {
        [SerializeField]
        private UpgradeConfig[] _configs;
        
        public UpgradeConfig[] GetAllUpgrades()
        {
            return _configs;
        }

        public UpgradeConfig FindUpgrade(string id)
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