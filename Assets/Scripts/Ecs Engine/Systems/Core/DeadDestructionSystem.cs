using System.Security.Cryptography;
using Common;
using EcsEngine.Components.Events;
using EcsEngine.Extensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems
{
    public sealed class DeadDestructionSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DeathEvent>> _filter;
        private readonly EcsCustomInject<ServiceLocator> _serviceLocatorInject;
        private readonly EcsWorldInject _world;
        
        private EntityManager _entityManager;
        
        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _entityManager = _serviceLocatorInject.Value.Resolve<EntityManager>();
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int entityId in _filter.Value)
            {
                _entityManager.Destroy(entityId);
            }
        }
    }
}