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
        private readonly EcsFilterInject<Inc<SpawnRequest, Spawn>> _filterInject = EcsWorldsAPI.EventsWorld;
        private readonly EcsPoolInject<SpawnFactoryRequest> _spawnFactoryRequestPoolInject = EcsWorldsAPI.EventsWorld;
        private readonly EcsWorldInject _eventsWorldInject = EcsWorldsAPI.EventsWorld;

        private readonly EcsPoolInject<SpawnAdjustable> _adjustablePoolInject;
        
        private readonly EcsZenject<EntityManager> _entityManagerInject;
        
        private EcsPool<SpawnRequest> _spawnRequestPool;
        private EcsPool<Spawn> _spawnPool;
        
        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _spawnRequestPool = _filterInject.Pools.Inc1;
            _spawnPool = _filterInject.Pools.Inc2;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int eventId in _filterInject.Value)
            {
                Spawn spawn = _spawnPool.Get(eventId);

                Entity prefab = spawn.Prefab;
                Vector3 position = spawn.SpawnPoint.position;
                Quaternion rotation = spawn.SpawnPoint.localRotation;

                Entity spawnedEntity = _entityManagerInject.Value.CreateEntity(prefab, position, rotation, out int spawnedEntityId);

                if (_adjustablePoolInject.Value.Has(spawnedEntityId))
                {
                    int factoryRequestId = _eventsWorldInject.Value.NewEntity();
                
                    _spawnRequestPool.Del(eventId);
                    _spawnPool.Del(eventId);
                
                    _eventsWorldInject.Value.CopyEntity(eventId, factoryRequestId);
                    _spawnFactoryRequestPoolInject.Value.Add(factoryRequestId) = new SpawnFactoryRequest { SpawnedEntity = spawnedEntity.PackedEntity};
                }
                
                _eventsWorldInject.Value.DelEntity(eventId);
            }
        }
    }
}