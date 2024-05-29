using JetBrains.Annotations;
using Upgrades.UpgradingLogic;

namespace Upgrades.Installation
{
    [UsedImplicitly]
    internal sealed class ConditionsInstaller
    { 
        internal void InstallConditions(Upgrade[] upgrades)
        {
            for (int i = 0; i < upgrades.Length; i++)
            {
                if (upgrades[i] is IConditionalUpgrade conditionalUpgrade)
                    conditionalUpgrade.HandleConditions(upgrades);
            }
        }
    }
}