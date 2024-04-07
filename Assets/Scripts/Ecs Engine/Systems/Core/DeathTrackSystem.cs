using System;
using EcsEngine.Components;
using EcsEngine.Components.Events;
using EcsEngine.Components.Requests;
using EcsEngine.Components.Tags;
using EcsEngine.Extensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems
{
    public sealed class DeathTrackSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DealDamageEvent, Target>> _filter = EcsWorldsAPI.EventsWorld;
        private readonly EcsPoolInject<Health> _healthPoolInject;
        private readonly EcsPoolInject<Inactive> _inactivePoolInject;
        private readonly EcsPoolInject<DeathRequest> _deathRequestPoolInject;
        private readonly EcsWorldInject _world;
        
        private EcsPool<Target> _targetPool;
        private EcsPool<Health> _healthPool;
        private EcsPool<Inactive> _inactivePool;
        private EcsPool<DeathRequest> _deathRequestPool;
        
        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _targetPool = _filter.Pools.Inc2;
            _healthPool = _healthPoolInject.Value;
            _inactivePool = _inactivePoolInject.Value;
            _deathRequestPool = _deathRequestPoolInject.Value;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int eventId in _filter.Value)
            {
                EcsPackedEntity targetEntity = _targetPool.Get(eventId).Value;
                
                if (!targetEntity.Unpack(_world.Value, out int targetEntityId)) 
                    throw new Exception("Target entity is unable to unpack");
                
                if (_inactivePool.Has(targetEntityId)) continue;
                
                Health health = _healthPool.Get(targetEntityId);
                
                if (health.HitPoints <= health.MinHitPoints) 
                    _deathRequestPool.Add(targetEntityId) = new DeathRequest();
            }
        }
    }
}