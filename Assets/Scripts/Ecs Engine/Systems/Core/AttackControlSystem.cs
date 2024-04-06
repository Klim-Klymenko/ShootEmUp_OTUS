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
    public sealed class AttackControlSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Attack, Position, Target, UnitState>, Exc<Inactive>> _filter;
        private readonly EcsPoolInject<AttackEnabled> _attackEnabledPoolInject;
        private readonly EcsWorldInject _gameObjectsWorld;
        
        private readonly EcsWorldInject _eventsWorld = EcsWorldsAPI.EventsWorld;
        private readonly EcsPoolInject<AttackRequest> _attackRequestPoolInject = EcsWorldsAPI.EventsWorld;
        private readonly EcsPoolInject<Target> _targetPoolInject = EcsWorldsAPI.EventsWorld;
        private readonly EcsPoolInject<Source> _sourcePoolInject = EcsWorldsAPI.EventsWorld;

        private EcsPool<AttackEnabled> _attackEnabledPool;
        private EcsPool<Attack> _attackPool;
        private EcsPool<Position> _positionPool;
        private EcsPool<Target> _targetPool;
        private EcsPool<UnitState> _unitStatePool;
        
        private EcsPool<AttackRequest> _attackRequestPool;
        private EcsPool<Target> _targetRequestPool;
        private EcsPool<Source> _sourceRequestPool;
        
        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _attackEnabledPool = _attackEnabledPoolInject.Value;
            _attackPool = _filter.Pools.Inc1;
            _positionPool = _filter.Pools.Inc2;
            _targetPool = _filter.Pools.Inc3;
            _unitStatePool = _filter.Pools.Inc4;
            
            _attackRequestPool = _attackRequestPoolInject.Value;
            _targetRequestPool = _targetPoolInject.Value;
            _sourceRequestPool = _sourcePoolInject.Value;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int entityId in _filter.Value)
            {
                float attackRange = _attackPool.Get(entityId).Range;
                Vector3 position = _positionPool.Get(entityId).Value;
                EcsPackedEntity targetEntity = _targetPool.Get(entityId).Value;
                ref State unitState = ref _unitStatePool.Get(entityId).Value;#
                    
                if (!targetEntity.Unpack(_gameObjectsWorld.Value, out int targetEntityId)) 
                    throw new Exception("Target entity is unable to unpack"); ;
                
                Vector3 targetPosition = _positionPool.Get(targetEntityId).Value;

                Vector3 distanceVector = targetPosition - position;
                float sqrDistance = distanceVector.sqrMagnitude;
                float sqrAttackRange = attackRange * attackRange;

                if (sqrDistance > sqrAttackRange)
                {
                    if (_attackEnabledPool.Has(entityId))
                        _attackEnabledPool.Del(entityId);
                    
                    unitState = State.Move;
                    
                    continue;
                }
                
                EcsPackedEntity sourceEntity = _gameObjectsWorld.Value.PackEntity(entityId);
                
                int attackEventId = _eventsWorld.Value.NewEntity();
                _attackRequestPool.Add(attackEventId) = new AttackRequest();
                _targetRequestPool.Add(attackEventId) = new Target { Value = targetEntity };
                _sourceRequestPool.Add(attackEventId) = new Source { Value = sourceEntity };
            }
        }
    }
}