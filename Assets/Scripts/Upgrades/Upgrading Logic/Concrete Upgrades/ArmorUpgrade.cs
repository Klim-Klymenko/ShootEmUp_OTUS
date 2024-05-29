using GameEngine;
using JetBrains.Annotations;
using Upgrades.Configs;
using Zenject;

namespace Upgrades.UpgradingLogic
{
    [UsedImplicitly]
    internal sealed class ArmorUpgrade : Upgrade
    {
        private PlayerStats _playerStats;
        private readonly ArmorUpgradeConfig _config;
        
        internal ArmorUpgrade(ArmorUpgradeConfig config) : base(config)
        {
            _config = config;
        }

        [Inject]
        internal void Construct(PlayerStats playerStats)
        {
            _playerStats = playerStats;
        }

        private protected override void LevelUp(int level)
        {
            int upgradedArmorValue = _config.GetArmor(level);
            _playerStats.ChangeStat(Id, upgradedArmorValue);
        }
    }
}