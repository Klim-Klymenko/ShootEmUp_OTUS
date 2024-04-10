using EcsEngine.Components;
using EcsEngine.Components.Requests;
using EcsEngine.Components.Tags;
using EcsEngine.Extensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    public sealed class HitRequestSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<HitRequest, Source, Target>> _filter = EcsWorldsAPI.EventsWorld;
        private readonly EcsPoolInject<DealDamageRequest> _dealDamageRequestPoolInject = EcsWorldsAPI.EventsWorld;
        private readonly EcsWorldInject _eventsWorld = EcsWorldsAPI.EventsWorld;
        
        private readonly EcsWorldInject _gameObjectsWorld;
        private readonly EcsPoolInject<Inactive> _inactivePoolInject;
        private readonly EcsPoolInject<AttackRange> _attackRangePoolInject;
        private readonly EcsPoolInject<Position> _positionPoolInject;
                
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
                Source source = _sourcePool.Get(eventId);
                Target target = _targetPool.Get(eventId);
                
                if (!source.Value.Unpack(_gameObjectsWorld.Value, out int sourceEntityId)) continue;
                if (!target.Value.Unpack(_gameObjectsWorld.Value, out int targetEntityId)) continue;
                
                if (_inactivePoolInject.Value.Has(sourceEntityId) || _inactivePoolInject.Value.Has(targetEntityId)) continue;
                
                Vector3 sourcePosition = _positionPoolInject.Value.Get(sourceEntityId).Value;
                Vector3 targetPosition = _positionPoolInject.Value.Get(targetEntityId).Value;
                
                float attackRange = _attackRangePoolInject.Value.Get(sourceEntityId).Value;
                float sqrAttackRange = attackRange * attackRange;
                
                Vector3 distanceVector = sourcePosition - targetPosition;
                float sqrDistance = distanceVector.sqrMagnitude;
                
                if (sqrDistance > sqrAttackRange) continue;
                
                int dealDamageRequestId = _eventsWorld.Value.NewEntity();
                
                _sourcePool.Add(dealDamageRequestId) = source;
                _targetPool.Add(dealDamageRequestId) = target;
                _dealDamageRequestPoolInject.Value.Add(dealDamageRequestId) = new DealDamageRequest();
                
                _eventsWorld.Value.DelEntity(eventId);
            }        
        }
    }
}