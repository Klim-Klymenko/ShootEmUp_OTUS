using System;
using EcsEngine.Components;
using EcsEngine.Components.Requests;
using EcsEngine.Components.Tags;
using EcsEngine.Extensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems
{
    public sealed class ShootRequestSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<ShootRequest, Source, Target>> _filterInject = EcsWorldsAPI.EventsWorld;
        private readonly EcsPoolInject<SpawnRequest> _spawnRequestPoolInject = EcsWorldsAPI.EventsWorld;
        private readonly EcsPoolInject<Spawn> _spawnEventPoolInject = EcsWorldsAPI.EventsWorld;
        private readonly EcsPoolInject<Damage> _damageRequestPoolInject = EcsWorldsAPI.EventsWorld;
        private readonly EcsWorldInject _eventsWorldInject = EcsWorldsAPI.EventsWorld;
        
        private readonly EcsWorldInject _gameObjectsWorldInject;
        private readonly EcsPoolInject<Spawn> _spawnPoolInject;
        private readonly EcsPoolInject<Inactive> _inactivePoolInject;
        private readonly EcsPoolInject<CurrentWeapon> _weaponPoolInject;
        private readonly EcsPoolInject<Damage> _damagePoolInject;
        
        private EcsPool<Source> _sourcePool;
        private EcsPool<Target> _targetPool;
        
        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _sourcePool = _filterInject.Pools.Inc2;
            _targetPool = _filterInject.Pools.Inc3;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int eventId in _filterInject.Value)
            {
                Source source = _sourcePool.Get(eventId);
                Target target = _targetPool.Get(eventId);
                
                if (!source.Value.Unpack(_gameObjectsWorldInject.Value, out int sourceEntityId))
                    throw new Exception("Source entity is unable to unpack");
                
                if (!target.Value.Unpack(_gameObjectsWorldInject.Value, out int targetEntityId))
                    throw new Exception("Target entity is unable to unpack");
                
                if (_inactivePoolInject.Value.Has(sourceEntityId) || _inactivePoolInject.Value.Has(targetEntityId)) continue;
                
                CurrentWeapon weapon = _weaponPoolInject.Value.Get(sourceEntityId);
                
                if (!weapon.Value.Unpack(_gameObjectsWorldInject.Value, out int weaponEntityId)) 
                    throw new Exception("Weapon entity is unable to unpack");

                Damage damage = _damagePoolInject.Value.Get(weaponEntityId);
                Spawn spawn = _spawnPoolInject.Value.Get(weaponEntityId);
                
                int requestId = _eventsWorldInject.Value.NewEntity();
                
                _targetPool.Add(requestId) = target;
                _damageRequestPoolInject.Value.Add(requestId) = damage;
                _spawnEventPoolInject.Value.Add(requestId) = spawn;
                _spawnRequestPoolInject.Value.Add(requestId) = new SpawnRequest();
                
                _eventsWorldInject.Value.DelEntity(eventId);
            }
        }
    }
}