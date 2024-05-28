using JetBrains.Annotations;
using Sample;
using Zenject;

namespace Upgrades.Installation
{
    [UsedImplicitly]
    internal sealed class UpgradesInstaller : IInitializable
    {
        private readonly Common.IFactory<Upgrade[]> _upgradesFactory;
        private readonly UpgradesManager _upgradesManager;

        internal UpgradesInstaller(Common.IFactory<Upgrade[]> upgradesFactory, UpgradesManager upgradesManager)
        {
            _upgradesFactory = upgradesFactory;
            _upgradesManager = upgradesManager;
        }

        void IInitializable.Initialize()
        {
            Upgrade[] upgrades = _upgradesFactory.Create();
            _upgradesManager.Setup(upgrades);
        }
    }
}