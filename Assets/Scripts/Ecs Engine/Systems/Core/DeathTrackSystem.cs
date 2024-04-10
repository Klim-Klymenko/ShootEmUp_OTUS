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
        private readonly EcsFilterInject<Inc<DealDamageEvent, Target>> _filterInject = EcsWorldsAPI.EventsWorld;
        private readonly EcsPoolInject<Health> _healthPoolInject;
        private readonly EcsPoolInject<Inactive> _inactivePoolInject;
        private readonly EcsPoolInject<DeathRequest> _deathRequestPoolInject;
        private readonly EcsWorldInject _worldInject;
        
        private EcsPool<Target> _targetPool;
        
        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _targetPool = _filterInject.Pools.Inc2;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int eventId in _filterInject.Value)
            {
                EcsPackedEntity targetEntity = _targetPool.Get(eventId).Value;
                
                if (!targetEntity.Unpack(_worldInject.Value, out int targetEntityId)) 
                    throw new Exception("Target entity is unable to unpack");
                
                if (_inactivePoolInject.Value.Has(targetEntityId)) continue;
                if (_deathRequestPoolInject.Value.Has(targetEntityId)) continue;
                
                Health health = _healthPoolInject.Value.Get(targetEntityId);
                
                if (health.HitPoints <= health.MinHitPoints) 
                    _deathRequestPoolInject.Value.Add(targetEntityId) = new DeathRequest();
            }
        }
    }
}