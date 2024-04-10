using Common;
using EcsEngine.Components;
using EcsEngine.Components.Requests;
using EcsEngine.Components.Tags;
using EcsEngine.Extensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    public sealed class SpawnRequestSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<SpawnRequest, Spawn>> _filter = EcsWorldsAPI.EventsWorld;
        private readonly EcsPoolInject<SpawnFactoryRequest> _spawnFactoryRequestPoolInject = EcsWorldsAPI.EventsWorld;
        private readonly EcsWorldInject _eventsWorld = EcsWorldsAPI.EventsWorld;

        private readonly EcsPoolInject<SpawnAdjustable> _adjustablePoolInject;
        
        private readonly EcsCustomInject<ServiceLocator> _serviceLocator;
        
        private EcsPool<SpawnRequest> _spawnRequestPool;
        private EcsPool<Spawn> _spawnPool;
        
        private EntityManager _entityManager;
        
        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _spawnRequestPool = _filter.Pools.Inc1;
            _spawnPool = _filter.Pools.Inc2;
            
            _entityManager = _serviceLocator.Value.Resolve<EntityManager>();
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int eventId in _filter.Value)
            {
                Spawn spawn = _spawnPool.Get(eventId);

                Entity prefab = spawn.Prefab;
                Vector3 position = spawn.SpawnPoint.position;
                Quaternion rotation = spawn.SpawnPoint.localRotation;

                Entity spawnedEntity = _entityManager.CreateEntity(prefab, position, rotation, out int spawnedEntityId);

                if (_adjustablePoolInject.Value.Has(spawnedEntityId))
                {
                    int factoryRequestId = _eventsWorld.Value.NewEntity();
                
                    _spawnRequestPool.Del(eventId);
                    _spawnPool.Del(eventId);
                
                    _eventsWorld.Value.CopyEntity(eventId, factoryRequestId);
                    _spawnFactoryRequestPoolInject.Value.Add(factoryRequestId) = new SpawnFactoryRequest { SpawnedEntity = spawnedEntity.PackedEntity};
                }
                
                _eventsWorld.Value.DelEntity(eventId);
            }
        }
    }
}