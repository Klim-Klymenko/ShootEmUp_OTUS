using System;
using Common.GameEngine;
using EcsEngine.Components;
using EcsEngine.Components.Events;
using EcsEngine.Components.Requests;
using EcsEngine.Components.Tags;
using EcsEngine.Extensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems
{
    public sealed class AttackEventSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AttackEvent, CurrentWeapon, Target, AttackEnabled>, Exc<Inactive>> _filterInject;
        private readonly EcsPoolInject<WeaponType> _weaponTypePoolInject;
        private readonly EcsWorldInject _gameObjectsWorldInject;

        private readonly EcsWorldInject _eventsWorldInject = EcsWorldsAPI.EventsWorld;
        private readonly EcsPoolInject<ShootRequest> _shootRequestPoolInject = EcsWorldsAPI.EventsWorld;
        private readonly EcsPoolInject<HitRequest> _hitRequestPoolInject = EcsWorldsAPI.EventsWorld;
        private readonly EcsPoolInject<Source> _sourcePoolInject = EcsWorldsAPI.EventsWorld;
        private readonly EcsPoolInject<Target> _targetPoolInject = EcsWorldsAPI.EventsWorld;

        private EcsPool<CurrentWeapon> _weaponPool;
        private EcsPool<Target> _targetPool;
        
        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _weaponPool = _filterInject.Pools.Inc2;
            _targetPool = _filterInject.Pools.Inc3;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int entityId in _filterInject.Value)
            {
                EcsPackedEntity sourceEntity = _gameObjectsWorldInject.Value.PackEntity(entityId);
                EcsPackedEntity targetEntity = _targetPool.Get(entityId).Value;
                EcsPackedEntity weaponEntity = _weaponPool.Get(entityId).Value;

                if (!weaponEntity.Unpack(_gameObjectsWorldInject.Value, out int weaponEntityId)) 
                    throw new Exception("Weapon entity is unable to unpack");

                Weapon weaponType = _weaponTypePoolInject.Value.Get(weaponEntityId).Value;
                
                int eventId = _eventsWorldInject.Value.NewEntity();
                
                _sourcePoolInject.Value.Add(eventId) = new Source { Value = sourceEntity };
                _targetPoolInject.Value.Add(eventId) = new Target { Value = targetEntity };

                if (weaponType == Weapon.SteelArms)
                    _hitRequestPoolInject.Value.Add(eventId) = new HitRequest();
                
                else if (weaponType == Weapon.FireArms)
                    _shootRequestPoolInject.Value.Add(eventId) = new ShootRequest();
            }
        }
    }
}