using Zenject;

namespace Domain
{
    internal class DomainInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSaveLoaders();
            BindUnitsSpawner();
            BindUnitDataConverter();
            BindUnitsFacade();
        }

        private void BindSaveLoaders()
        {
            Container.BindInterfacesTo<UnitSaveLoader>().AsCached();
            Container.BindInterfacesTo<ResourcesSaveLoader>().AsCached();
        }

        private void BindUnitsSpawner()
        {
            Container.Bind<UnitsSpawner>().AsSingle();
        }

        private void BindUnitDataConverter()
        {
            Container.Bind<UnitsDataConverter>().AsSingle();
        }

        private void BindUnitsFacade()
        {
            Container.Bind<UnitsFacade>().AsSingle();
        }
    }
}