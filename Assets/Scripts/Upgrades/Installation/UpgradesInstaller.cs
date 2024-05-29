using JetBrains.Annotations;
using Upgrades.UpgradingLogic;
using Zenject;

namespace Upgrades.Installation
{
    [UsedImplicitly]
    internal sealed class UpgradesInstaller : IInitializable
    {
        private readonly Common.IFactory<Upgrade[]> _upgradesFactory;
        private readonly ConditionsInstaller _conditionsInstaller;
        private readonly UpgradesManager _upgradesManager;

        internal UpgradesInstaller(Common.IFactory<Upgrade[]> upgradesFactory, ConditionsInstaller conditionsInstaller, UpgradesManager upgradesManager)
        {
            _upgradesFactory = upgradesFactory;
            _conditionsInstaller = conditionsInstaller;
            _upgradesManager = upgradesManager;
        }

        void IInitializable.Initialize()
        {
            Upgrade[] upgrades = _upgradesFactory.Create();
            
            _conditionsInstaller.InstallConditions(upgrades);
            _upgradesManager.Setup(upgrades);
        }
    }
}