using System;
using Common;
using EcsEngine.Components;
using EcsEngine.Components.Requests;
using EcsEngine.Extensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    public sealed class SpawnRequestSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<SpawnRequest, Spawn>> _filter = EcsWorldsAPI.EventsWorld;
        private readonly EcsPoolInject<Target> _targetRequestPoolInject = EcsWorldsAPI.EventsWorld;
        private readonly EcsPoolInject<CurrentWeapon> _weaponRequestPoolInject = EcsWorldsAPI.EventsWorld;
        private readonly EcsWorldInject _eventsWorld = EcsWorldsAPI.EventsWorld;
        
        private readonly EcsPoolInject<Target> _targetPoolInject;
        private readonly EcsPoolInject<CurrentWeapon> _weaponPoolInject;
        
        private readonly EcsCustomInject<ServiceLocator> _serviceLocator;
        
        private EcsPool<Spawn> _spawnPool;
        private EntityManager _entityManager;
        
        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
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

                _entityManager.CreateEntity(prefab, position, rotation, out int spawnedEntityId);
                
                if (_targetRequestPoolInject.Value.Has(eventId))
                    _targetPoolInject.Value.Add(spawnedEntityId) = _targetRequestPoolInject.Value.Get(eventId);
                
                if (_weaponRequestPoolInject.Value.Has(eventId))
                    _weaponPoolInject.Value.Add(spawnedEntityId) = _weaponRequestPoolInject.Value.Get(eventId);
                
                _eventsWorld.Value.DelEntity(eventId);
            }
        }
    }
}