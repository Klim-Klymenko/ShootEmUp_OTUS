using GameEngine;
using Upgrades.Configs;
using Zenject;

namespace Upgrades.UpgradingLogic
{
    internal sealed class DamageUpgrade : Upgrade
    {
        private PlayerStats _playerStats;
        private readonly DamageUpgradeConfig _config;
        
        internal DamageUpgrade(DamageUpgradeConfig config) : base(config)
        {
            _config = config;
        }

        [Inject]
        internal void Construct(PlayerStats playerStats)
        {
            _playerStats = playerStats;
        }
        
        private protected override void LevelUp(int upgradedLevel)
        {
            int upgradedDamage = _config.GetDamage(upgradedLevel);
            _playerStats.ChangeStat(Id, upgradedDamage);
        }
    }
}