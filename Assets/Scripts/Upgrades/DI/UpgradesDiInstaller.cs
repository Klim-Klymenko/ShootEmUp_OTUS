using Sample;
using UnityEngine;
using Upgrades.Installation;
using Zenject;

namespace Upgrades.DI
{
    internal sealed class UpgradesDiInstaller : MonoInstaller
    {
        [SerializeField]
        private UpgradeCatalog _upgradeCatalog;
        
        public override void InstallBindings()
        {
            BindConfigs();
            BindUpgradesFactory();
            BindUpgradesInstaller();
            BindUpgradesManager();
        }

        private void BindConfigs()
        {
            Container.Bind<UpgradeCatalog>().FromInstance(_upgradeCatalog).AsSingle();
        }

        private void BindUpgradesFactory()
        {
            Container.Bind<Common.IFactory<Upgrade[]>>().To<UpgradesFactory>().AsSingle();
        }
        
        private void BindUpgradesInstaller()
        {
            Container.BindInterfacesTo<UpgradesInstaller>().AsCached();
        }
        
        private void BindUpgradesManager()
        {
            Container.Bind<UpgradesManager>().AsSingle();
        }
    }
}