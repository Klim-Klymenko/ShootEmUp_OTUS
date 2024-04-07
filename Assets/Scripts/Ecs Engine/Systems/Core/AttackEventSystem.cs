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
        private readonly EcsFilterInject<Inc<AttackEvent, CurrentWeapon, Target>, Exc<Inactive>> _filter;
        private readonly EcsPoolInject<WeaponType> _weaponTypePoolInject;
        private readonly EcsWorldInject _gameObjectsWorld;

        private readonly EcsWorldInject _eventsWorld = EcsWorldsAPI.EventsWorld;
        private readonly EcsPoolInject<ShootRequest> _shootRequestPoolInject = EcsWorldsAPI.EventsWorld;
        private readonly EcsPoolInject<DealDamageRequest> _dealDamageRequestPoolInject = EcsWorldsAPI.EventsWorld;
        private readonly EcsPoolInject<Source> _sourcePoolInject = EcsWorldsAPI.EventsWorld;
        private readonly EcsPoolInject<Target> _targetPoolInject = EcsWorldsAPI.EventsWorld;

        private EcsPool<CurrentWeapon> _weaponPool;
        private EcsPool<Target> _targetPool;
        private EcsPool<WeaponType> _weaponTypePool;
        
        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _weaponPool = _filter.Pools.Inc2;
            _targetPool = _filter.Pools.Inc3;
            _weaponTypePool = _weaponTypePoolInject.Value;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int entityId in _filter.Value)
            {
                EcsPackedEntity weaponEntity = _weaponPool.Get(entityId).Value;
                EcsPackedEntity targetEntity = _targetPool.Get(entityId).Value;
                EcsPackedEntity sourceEntity = _gameObjectsWorld.Value.PackEntity(entityId);

                if (!targetEntity.Unpack(_gameObjectsWorld.Value, out int targetEntityId))
                    throw new Exception("Target entity is unable to unpack");
                
                if (!weaponEntity.Unpack(_gameObjectsWorld.Value, out int weaponEntityId)) 
                    throw new Exception("Weapon entity is unable to unpack");

                Weapon weaponType = _weaponTypePool.Get(weaponEntityId).Value;
                
                int eventId = _eventsWorld.Value.NewEntity();
                
                _sourcePoolInject.Value.Add(eventId) = new Source {Value = sourceEntity};
                _targetPoolInject.Value.Add(eventId) = new Target {Value = targetEntity};

                if (weaponType == Weapon.SteelArms)
                    _dealDamageRequestPoolInject.Value.Add(eventId) = new DealDamageRequest();
                
                else if (weaponType == Weapon.FireArms)
                    _shootRequestPoolInject.Value.Add(eventId) = new ShootRequest();
            }
        }
    }
}