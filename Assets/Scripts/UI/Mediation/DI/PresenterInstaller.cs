using Zenject;

namespace PM
{
    internal sealed class PresenterInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindPresenters();
        }

        private void BindPresenters()
        {
            Container.BindInterfacesTo<CharacterStatsPresenter>().AsCached();
            Container.BindInterfacesTo<PlayerLevelPresenter>().AsCached();
            Container.BindInterfacesTo<UserInfoPresenter>().AsCached();
        }
    }
}