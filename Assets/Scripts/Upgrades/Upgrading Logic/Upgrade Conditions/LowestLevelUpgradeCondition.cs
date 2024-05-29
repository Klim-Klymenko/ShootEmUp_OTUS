using System.Collections.Generic;
using Common;
using JetBrains.Annotations;

namespace Upgrades.UpgradingLogic
{
    [UsedImplicitly]
    internal sealed class LowestLevelUpgradeCondition : ICondition
    {
        private readonly Upgrade _targetUpgrade;
        private readonly IReadOnlyList<Upgrade> _dependentUpgrades;

        internal LowestLevelUpgradeCondition(Upgrade targetUpgrade, IReadOnlyList<Upgrade> dependentUpgrades)
        {
            _targetUpgrade = targetUpgrade;
            _dependentUpgrades = dependentUpgrades;
        }

        bool ICondition.Invoke()
        {
            int levelToUpgrade = _targetUpgrade.Level + 1;

            for (int i = 0; i < _dependentUpgrades.Count; i++)
            {
                int  dependentUpgradeLevel = _dependentUpgrades[i].Level;
                
                if (dependentUpgradeLevel < levelToUpgrade)
                    return false;
            }

            return true;
        }
    }
}