using System.Collections.Generic;
using Common;
using Upgrades.UpgradingLogic;

namespace Upgrades.Installation
{
    public interface IConditionsFactory
    {
        ICondition[] Create(Upgrade targetUpgrade, IReadOnlyList<Upgrade> dependentUpgrades);
    }
}