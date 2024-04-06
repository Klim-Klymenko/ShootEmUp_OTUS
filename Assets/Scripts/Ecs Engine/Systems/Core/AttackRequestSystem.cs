using System;
using EcsEngine.Components;
using EcsEngine.Components.Requests;
using EcsEngine.Components.Tags;
using EcsEngine.Data;
using EcsEngine.Extensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    public sealed class AttackRequestSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AttackRequest, Target, Source>> _filter = EcsWorldsAPI.EventsWorld;
        private readonly EcsWorldInject _eventsWorld = EcsWorldsAPI.EventsWorld;
        private readonly EcsWorldInject _gameObjectsWorld;
        
        private readonly EcsPoolInject<UnitState> _unitStatePoolInject;
        private readonly EcsPoolInject<Timer> _timerPoolInject;
        private readonly EcsPoolInject<Tickable> _tickablePoolInject;
        private readonly EcsPoolInject<CurrentWeapon> _weaponPoolInject;
        private readonly EcsPoolInject<AttackRequest> _attackRequestPoolInject;
        private readonly EcsPoolInject<AttackEnabled> _attackEnabledPoolInject;

        private EcsPool<Source> _sourcePool;
        private EcsPool<UnitState> _unitStatePool;
        private EcsPool<Tickable> _tickablePool;
        private EcsPool<CurrentWeapon> _weaponPool;
        private EcsPool<AttackRequest> _attackRequestPool;
        private EcsPool<AttackEnabled> _attackEnabledPool;
        
        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _sourcePool = _filter.Pools.Inc3;
            _unitStatePool = _unitStatePoolInject.Value;
            _tickablePool = _tickablePoolInject.Value;
            _weaponPool = _weaponPoolInject.Value;
            _attackRequestPool = _attackRequestPoolInject.Value;
            _attackEnabledPool = _attackEnabledPoolInject.Value;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int eventId in _filter.Value)
            {
                EcsPackedEntity sourceEntity = _sourcePool.Get(eventId).Value;
                
                if (!sourceEntity.Unpack(_gameObjectsWorld.Value, out int sourceEntityId)) 
                    throw new Exception("Source entity is unable to unpack"); 
                
                ref State sourceState = ref _unitStatePool.Get(sourceEntityId).Value;

                EcsPackedEntity weaponEntity = _weaponPool.Get(sourceEntityId).Value;
                
                if (!weaponEntity.Unpack(_gameObjectsWorld.Value, out int weaponEntityId)) 
                    throw new Exception("Weapon entity is unable to unpack"); 
            
                if (_tickablePool.Has(weaponEntityId))
                {
                    _eventsWorld.Value.DelEntity(eventId);
                    continue;
                }
                
                if (sourceState != State.Attack)
                {
                    if (!_attackEnabledPool.Has(sourceEntityId))
                        _attackEnabledPool.Add(sourceEntityId) = new AttackEnabled();
                    
                    sourceState = State.Attack;
                }
                
                if (!_attackRequestPool.Has(sourceEntityId))
                    _attackRequestPool.Add(sourceEntityId) = new AttackRequest();
                
                if (!_tickablePool.Has(weaponEntityId))
                    _tickablePool.Add(weaponEntityId) = new Tickable();
                
                _eventsWorld.Value.DelEntity(eventId);
            }
        }
    }
}