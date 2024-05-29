using UnityEngine;
using Upgrades.Tables;
using Upgrades.UpgradingLogic;

namespace Upgrades.Configs
{
    [CreateAssetMenu(fileName = "DamageUpgradeConfig", menuName = "Configs/Upgrades/DamageUpgradeConfig")]
    public sealed class DamageUpgradeConfig : UpgradeConfig
    {
        [SerializeField]
        private StatUpgradeTable _damageUpgradeTable;

        private protected override void Validate()
        {
            base.Validate();
            _damageUpgradeTable.OnValidate(MaxLevel);
        }

        internal int GetDamage(int level)
        {
            return _damageUpgradeTable.GetStatValue(level);
        }
        
        internal override Upgrade InstantiateUpgrade()
        {
            return new DamageUpgrade(this);
        }
    }
}