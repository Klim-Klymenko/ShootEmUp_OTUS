using UnityEngine;
using Upgrades.Tables;
using Upgrades.UpgradingLogic;

namespace Upgrades.Configs
{
    [CreateAssetMenu(fileName = "HealthUpgradeConfig", menuName = "Configs/Upgrades/HealthUpgradeConfig")]
    public sealed class HealthUpgradeConfig : UpgradeConfig
    {
        [SerializeField]
        private StatUpgradeTable _healthUpgradeTable;
        
        private protected override void Validate()
        {
            base.Validate();
            _healthUpgradeTable.OnValidate(MaxLevel);
        }

        internal int GetHealth(int level)
        {
            return _healthUpgradeTable.GetStatValue(level);
        }
        
        internal override Upgrade InstantiateUpgrade()
        {
            return new HealthUpgrade(this);
        }
    }
}