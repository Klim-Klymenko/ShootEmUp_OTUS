﻿using System;
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
        private readonly EcsFilterInject<Inc<DealDamageRequest, Source, Target>> _filterInject = EcsWorldsAPI.EventsWorld;
        private readonly EcsPoolInject<DealDamageEvent> _dealDamageEventPoolInject = EcsWorldsAPI.EventsWorld;
        private readonly EcsWorldInject _eventsWorldInject = EcsWorldsAPI.EventsWorld;
        
        private readonly EcsWorldInject _gameObjectsWorldInject;
        private readonly EcsPoolInject<Health> _healthPoolInject;
        private readonly EcsPoolInject<Damage> _damagePoolInject;
        private readonly EcsPoolInject<CurrentWeapon> _weaponPoolInject;

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
                EcsPackedEntity sourceEntity = _sourcePool.Get(eventId).Value;
                EcsPackedEntity targetEntity = _targetPool.Get(eventId).Value;

                int damage;
                
                if (!sourceEntity.Unpack(_gameObjectsWorldInject.Value, out int sourceEntityId)) 
                    throw new Exception("Source entity is unable to unpack");
                
                if (_damagePoolInject.Value.Has(sourceEntityId))
                    damage = _damagePoolInject.Value.Get(sourceEntityId).Value;
                else
                {
                    EcsPackedEntity weaponEntity = _weaponPoolInject.Value.Get(sourceEntityId).Value;

                    if (!weaponEntity.Unpack(_gameObjectsWorldInject.Value, out int weaponEntityId))
                        throw new Exception("Weapon entity is unable to unpack");
                    
                    damage = _damagePoolInject.Value.Get(weaponEntityId).Value;
                }
                
                if (!targetEntity.Unpack(_gameObjectsWorldInject.Value, out int targetEntityId)) continue;
                
                ref Health health = ref _healthPoolInject.Value.Get(targetEntityId);
                
                health.HitPoints = Mathf.Max(health.MinHitPoints, health.HitPoints - damage);
                
                int dealDamageEventId = _eventsWorldInject.Value.NewEntity();
                
                _dealDamageEventPoolInject.Value.Add(dealDamageEventId) = new DealDamageEvent();
                _sourcePool.Add(dealDamageEventId) = new Source { Value = sourceEntity };
                _targetPool.Add(dealDamageEventId) = new Target { Value = targetEntity };
                
                _eventsWorldInject.Value.DelEntity(eventId);
            }
        }
    }
}