using Common;
using EcsEngine.Components.Events;
using EcsEngine.Extensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems
{
    public sealed class DeadDestructionSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DeathEvent>> _filterInject;
        private readonly EcsWorldInject _worldInject;
        
        private readonly EcsCustomInject<ServiceLocator> _serviceLocatorInject;
        private EntityManager _entityManager;
        
        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _entityManager = _serviceLocatorInject.Value.Resolve<EntityManager>();
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int entityId in _filterInject.Value)
            {
                _entityManager.Destroy(entityId);
            }
        }
    }
}