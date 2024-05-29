using UnityEngine;
using Upgrades.Configs;
using Upgrades.Installation;
using Upgrades.UpgradingLogic;
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
            BindUpgradesFactories();
            BindInstallers();
            BindUpgradesManager();
        }

        private void BindConfigs()
        {
            Container.Bind<UpgradeCatalog>().FromInstance(_upgradeCatalog).AsSingle();
        }

        private void BindUpgradesFactories()
        {
            Container.Bind<Common.IFactory<Upgrade[]>>().To<UpgradesFactory>().AsSingle();
            Container.Bind<IConditionsFactory>().To<ConditionsFactory>().AsSingle();
        }
        
        private void BindInstallers()
        {
            Container.BindInterfacesTo<UpgradesInstaller>().AsCached();
            Container.Bind<ConditionsInstaller>().AsSingle();
        }
        
        private void BindUpgradesManager()
        {
            Container.Bind<UpgradesManager>().AsSingle();
        }
    }
}