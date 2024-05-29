using System.Collections.Generic;
using Common;
using GameEngine;
using Upgrades.Configs;
using Upgrades.Installation;
using Zenject;

namespace Upgrades.UpgradingLogic
{
    internal sealed class SpeedUpgrade : Upgrade, IConditionalUpgrade
    {
        private PlayerStats _playerStats;
        private IConditionsFactory _conditionsFactory;
        private readonly SpeedUpgradeConfig _config;
        
        internal SpeedUpgrade(SpeedUpgradeConfig config) : base(config)
        {
            _config = config;
        }

        [Inject]
        internal void Construct(PlayerStats playerStats, IConditionsFactory conditionsFactory)
        {
            _playerStats = playerStats;
            _conditionsFactory = conditionsFactory;
        }

        void IConditionalUpgrade.HandleConditions(Upgrade[] upgrades)
        {
            List<Upgrade> dependentUpgrades = new();

            for (int i = 0; i < upgrades.Length; i++)
            {
                Upgrade upgrade = upgrades[i];
                
                if (upgrade != this)
                    dependentUpgrades.Add(upgrade);
            }
            
            ICondition[] conditions = _conditionsFactory.Create(this, dependentUpgrades);
            Conditions = conditions;
        }

        private protected override void LevelUp(int level)
        {
            int upgradedSpeed = _config.GetSpeed(level);
            _playerStats.ChangeStat(Id, upgradedSpeed);
        }
    }
}