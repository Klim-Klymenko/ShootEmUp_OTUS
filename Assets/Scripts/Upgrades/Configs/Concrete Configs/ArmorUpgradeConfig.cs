using UnityEngine;
using Upgrades.Tables;
using Upgrades.UpgradingLogic;

namespace Upgrades.Configs
{
    [CreateAssetMenu(fileName = "ArmorUpgradeConfig", menuName = "Configs/Upgrades/ArmorUpgradeConfig")]
    public sealed class ArmorUpgradeConfig : UpgradeConfig
    {
        [SerializeField]
        private StatUpgradeTable _armorUpgradeTable;

        private protected override void Validate()
        {
            base.Validate();
            _armorUpgradeTable.OnValidate(MaxLevel);
        }

        internal int GetArmor(int level)
        {
            return _armorUpgradeTable.GetStatValue(level);
        }
        
        internal override Upgrade InstantiateUpgrade()
        {
            return new ArmorUpgrade(this);
        }
    }
}