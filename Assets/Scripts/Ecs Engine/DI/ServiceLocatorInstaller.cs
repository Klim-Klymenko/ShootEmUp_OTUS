using Common;
using EcsEngine.Extensions;
using GameCycle;
using JetBrains.Annotations;

namespace EcsEngine.DI
{
    [UsedImplicitly]
    internal sealed class ServiceLocatorInstaller
    {
        internal ServiceLocatorInstaller(ServiceLocator serviceLocator, EntityManager entityManager, GameCycleManager gameCycleManager)
        {
            serviceLocator.Bind(entityManager);
            serviceLocator.Bind(gameCycleManager);
        }
    }
}