using UnityEngine;
using Upgrades.Tables;
using Upgrades.UpgradingLogic;

namespace Upgrades.Configs
{
    [CreateAssetMenu(fileName = "SpeedUpgradeConfig", menuName = "Configs/Upgrades/SpeedUpgradeConfig")]
    public sealed class SpeedUpgradeConfig : UpgradeConfig
    {
        [SerializeField]
        private StatUpgradeTable _speedUpgradeTable;

        private protected override void Validate()
        {
            base.Validate();
            _speedUpgradeTable.OnValidate(MaxLevel);
        }

        internal int GetSpeed(int level)
        {
            return _speedUpgradeTable.GetStatValue(level);
        }
        
        internal override Upgrade InstantiateUpgrade()
        {
            return new SpeedUpgrade(this);
        }
    }
}