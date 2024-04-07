using System;
using EcsEngine.Components;
using EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    public sealed class AttackTrackSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Attack, Position, Target>, Exc<Inactive>> _filter;
        private readonly EcsPoolInject<AttackEnabled> _attackEnabledPoolInject;
        private readonly EcsWorldInject _gameObjectsWorld;

        private EcsPool<Attack> _attackPool;
        private EcsPool<Position> _positionPool;
        private EcsPool<Target> _targetPool;
        private EcsPool<AttackEnabled> _attackEnabledPool;

        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _attackPool = _filter.Pools.Inc1;
            _positionPool = _filter.Pools.Inc2;
            _targetPool = _filter.Pools.Inc3;
            _attackEnabledPool = _attackEnabledPoolInject.Value;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int entityId in _filter.Value)
            {
                float attackRange = _attackPool.Get(entityId).Range;
                Vector3 position = _positionPool.Get(entityId).Value;
                EcsPackedEntity targetEntity = _targetPool.Get(entityId).Value; 
                    
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
                    
                    continue;
                }
                
                if (_attackEnabledPool.Has(entityId)) continue;
                
                _attackEnabledPool.Add(entityId) = new AttackEnabled();
            }
        }
    }
}