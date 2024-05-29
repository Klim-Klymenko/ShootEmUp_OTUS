using System.Collections.Generic;
using JetBrains.Annotations;
using Upgrades.Configs;
using Upgrades.UpgradingLogic;
using Zenject;

namespace Upgrades.Installation
{
    [UsedImplicitly]
    internal sealed class UpgradesFactory : Common.IFactory<Upgrade[]>
    {
        private readonly UpgradeCatalog _upgradeCatalog;
        private readonly DiContainer _diContainer;

        internal UpgradesFactory(UpgradeCatalog upgradeCatalog, DiContainer diContainer)
        {
            _upgradeCatalog = upgradeCatalog;
            _diContainer = diContainer;
        }

        Upgrade[] Common.IFactory<Upgrade[]>.Create()
        {
            IReadOnlyList<UpgradeConfig> upgradeConfigs = _upgradeCatalog.GetAllUpgradeConfigs();

            Upgrade[] upgrades = new Upgrade[upgradeConfigs.Count];
            
            for (int i = 0; i < upgradeConfigs.Count; i++)
            { 
                Upgrade upgrade = upgradeConfigs[i].InstantiateUpgrade();
                _diContainer.Inject(upgrade);
                
                upgrades[i] = upgrade;
            }

            return upgrades;
        }
    }
}