using Zenject;

namespace Domain
{
    internal class DomainInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSaveLoaders();
            BindUnitsFacade();
        }

        private void BindSaveLoaders()
        {
            Container.BindInterfacesTo<UnitSaveLoader>().AsCached();
            Container.BindInterfacesTo<ResourcesSaveLoader>().AsCached();
        }

        private void BindUnitsFacade()
        {
            Container.Bind<UnitsFacade>().AsSingle();
        }
    }
}