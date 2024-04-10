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
        private readonly EcsFilterInject<Inc<SpawnFactoryRequest>> _filterInject = EcsWorldsAPI.EventsWorld;
        private readonly EcsPoolInject<Target> _targetRequestPoolInject = EcsWorldsAPI.EventsWorld;
        private readonly EcsPoolInject<Damage> _damageRequestPoolInject = EcsWorldsAPI.EventsWorld;
        private readonly EcsWorldInject _eventsWorldInject = EcsWorldsAPI.EventsWorld;

        private readonly EcsWorldInject _gameObjectsWorldInject;
        private readonly EcsPoolInject<ProjectileTag> _projectileTagPoolInject;
        private readonly EcsPoolInject<Target> _targetPoolInject;
        private readonly EcsPoolInject<Damage> _damagePoolInject;
        
        private EcsPool<SpawnFactoryRequest> _spawnedEntityRequestPool;
        
        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _spawnedEntityRequestPool = _filterInject.Pools.Inc1;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int eventId in _filterInject.Value)
            {
                EcsPackedEntity spawnedEntity = _spawnedEntityRequestPool.Get(eventId).SpawnedEntity;
                
                if (!spawnedEntity.Unpack(_gameObjectsWorldInject.Value, out int spawnedEntityId)) 
                    throw new Exception("Spawned entity is unable to unpack");
                
                if (!_projectileTagPoolInject.Value.Has(spawnedEntityId)) continue;
                
                if (_targetRequestPoolInject.Value.Has(eventId))
                    _targetPoolInject.Value.Add(spawnedEntityId) = _targetRequestPoolInject.Value.Get(eventId);
                
                if (_damageRequestPoolInject.Value.Has(eventId))
                    _damagePoolInject.Value.Add(spawnedEntityId) = _damageRequestPoolInject.Value.Get(eventId);
                
                _eventsWorldInject.Value.DelEntity(eventId);
            }
        }
    }
}