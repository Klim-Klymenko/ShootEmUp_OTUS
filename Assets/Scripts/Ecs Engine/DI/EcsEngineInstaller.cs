using EcsEngine.Extensions;
using Zenject;

namespace EcsEngine.DI
{
    internal sealed class EcsEngineInstaller : MonoInstaller
    {
        private EntityManager _entityManager;
        
        public override void InstallBindings()
        {
            BindEntityManager();
            BindEcsStartup();
        }
        
        private void BindEntityManager()
        {
            Container.Bind<EntityManager>().AsSingle();
        }
        
        private void BindEcsStartup()
        {
            Container.BindInterfacesTo<EcsStartup>().AsSingle();
        }
    }
}