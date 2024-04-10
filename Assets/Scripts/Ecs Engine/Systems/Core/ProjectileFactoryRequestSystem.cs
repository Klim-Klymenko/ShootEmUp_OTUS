using System;
using EcsEngine.Components;
using EcsEngine.Components.Requests;
using EcsEngine.Components.Tags;
using EcsEngine.Extensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems
{
    public sealed class ProjectileFactoryRequestSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<SpawnFactoryRequest>> _filter = EcsWorldsAPI.EventsWorld;
        private readonly EcsPoolInject<Target> _targetRequestPoolInject = EcsWorldsAPI.EventsWorld;
        private readonly EcsPoolInject<Damage> _damageRequestPoolInject = EcsWorldsAPI.EventsWorld;
        private readonly EcsWorldInject _eventsWorld = EcsWorldsAPI.EventsWorld;

        private readonly EcsWorldInject _gameObjectsWorld;
        private readonly EcsPoolInject<ProjectileTag> _projectileTagPoolInject;
        private readonly EcsPoolInject<Target> _targetPoolInject;
        private readonly EcsPoolInject<Damage> _damagePoolInject;
        
        private EcsPool<SpawnFactoryRequest> _spawnedEntityRequestPool;
        
        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _spawnedEntityRequestPool = _filter.Pools.Inc1;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int requestId in _filter.Value)
            {
                EcsPackedEntity spawnedEntity = _spawnedEntityRequestPool.Get(requestId).SpawnedEntity;
                
                if (!spawnedEntity.Unpack(_gameObjectsWorld.Value, out int spawnedEntityId)) 
                    throw new Exception("Spawned entity is unable to unpack");
                
                if (!_projectileTagPoolInject.Value.Has(spawnedEntityId)) continue;
                
                if (_targetRequestPoolInject.Value.Has(requestId))
                    _targetPoolInject.Value.Add(spawnedEntityId) = _targetRequestPoolInject.Value.Get(requestId);
                
                if (_damageRequestPoolInject.Value.Has(requestId))
                    _damagePoolInject.Value.Add(spawnedEntityId) = _damageRequestPoolInject.Value.Get(requestId);
                
                _eventsWorld.Value.DelEntity(requestId);
            }
        }
    }
}