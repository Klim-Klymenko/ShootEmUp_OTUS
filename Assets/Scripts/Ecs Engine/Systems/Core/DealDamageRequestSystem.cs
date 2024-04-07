using System;
using EcsEngine.Components;
using EcsEngine.Components.Events;
using EcsEngine.Components.Requests;
using EcsEngine.Extensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    public sealed class DealDamageRequestSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DealDamageRequest, Source, Target>> _filter = EcsWorldsAPI.EventsWorld;
        private readonly EcsPoolInject<DealDamageEvent> _dealDamageEventPoolInject = EcsWorldsAPI.EventsWorld;
        private readonly EcsWorldInject _eventsWorld = EcsWorldsAPI.EventsWorld;
        private readonly EcsWorldInject _gameObjectsWorld;
        private readonly EcsPoolInject<Health> _healthPoolInject;
        private readonly EcsPoolInject<Damage> _damagePoolInject;
        private readonly EcsPoolInject<CurrentWeapon> _weaponPoolInject;
        
        private EcsPool<Source> _sourcePool;
        private EcsPool<Target> _targetPool;
        
        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _sourcePool = _filter.Pools.Inc2;
            _targetPool = _filter.Pools.Inc3;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int eventId in _filter.Value)
            {
                EcsPackedEntity sourceEntity = _sourcePool.Get(eventId).Value;
                EcsPackedEntity targetEntity = _targetPool.Get(eventId).Value;
                
                if (!sourceEntity.Unpack(_gameObjectsWorld.Value, out int sourceEntityId)) 
                    throw new Exception("Source entity is unable to unpack");
                
                if (!targetEntity.Unpack(_gameObjectsWorld.Value, out int targetEntityId)) 
                    throw new Exception("Target entity is unable to unpack");
                
                EcsPackedEntity weaponEntity = _weaponPoolInject.Value.Get(sourceEntityId).Value;
                
                if (!weaponEntity.Unpack(_gameObjectsWorld.Value, out int weaponEntityId)) 
                    throw new Exception("Weapon entity is unable to unpack");
                
                int damage = _damagePoolInject.Value.Get(weaponEntityId).Value;
                ref Health health = ref _healthPoolInject.Value.Get(targetEntityId);
                
                health.HitPoints = Mathf.Max(health.MinHitPoints, health.HitPoints - damage);
                
                int dealDamageEventId = _eventsWorld.Value.NewEntity();
                
                _dealDamageEventPoolInject.Value.Add(dealDamageEventId) = new DealDamageEvent();
                _sourcePool.Add(dealDamageEventId) = new Source { Value = sourceEntity };
                _targetPool.Add(dealDamageEventId) = new Target { Value = targetEntity };
                
                _eventsWorld.Value.DelEntity(eventId);
            }
        }
    }
}