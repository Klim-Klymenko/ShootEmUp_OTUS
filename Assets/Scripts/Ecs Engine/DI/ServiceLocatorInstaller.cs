using Common;
using GameCycle;
using JetBrains.Annotations;

namespace EcsEngine.Extensions
{
    [UsedImplicitly]
    public sealed class ServiceLocatorInstaller
    {
        public ServiceLocatorInstaller(ServiceLocator serviceLocator, EntityManager entityManager, GameCycleManager gameCycleManager)
        {
            serviceLocator.Bind(entityManager);
            serviceLocator.Bind(gameCycleManager);
        }
    }
}