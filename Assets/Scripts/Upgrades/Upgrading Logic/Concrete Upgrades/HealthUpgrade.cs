using GameEngine;
using JetBrains.Annotations;
using Upgrades.Configs;
using Zenject;

namespace Upgrades.UpgradingLogic
{
    [UsedImplicitly]
    internal sealed class HealthUpgrade : Upgrade
    {
        private  PlayerStats _playerStats;
        private readonly HealthUpgradeConfig _config;
        
        internal HealthUpgrade(HealthUpgradeConfig config) : base(config)
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
            int upgradedHealth = _config.GetHealth(upgradedLevel);
            _playerStats.ChangeStat(Id, upgradedHealth);
        }
    }
}