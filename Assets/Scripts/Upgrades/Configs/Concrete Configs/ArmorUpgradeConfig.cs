using Sample.ConcreteUpgrades;
using UnityEngine;

namespace Sample
{
    [CreateAssetMenu(fileName = "ArmorUpgradeConfig", menuName = "Configs/Upgrades/Armor Upgrade")]
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