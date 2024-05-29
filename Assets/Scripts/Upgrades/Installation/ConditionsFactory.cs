using System.Collections.Generic;
using JetBrains.Annotations;
using Common;
using Upgrades.UpgradingLogic;

namespace Upgrades.Installation
{
    [UsedImplicitly]
    internal sealed class ConditionsFactory : IConditionsFactory
    {
        ICondition[] IConditionsFactory.Create(Upgrade targetUpgrade, IReadOnlyList<Upgrade> dependentUpgrades)
        {
            return new ICondition[]
            {
                new LowestLevelUpgradeCondition(targetUpgrade, dependentUpgrades)
            };
        }
    }
}