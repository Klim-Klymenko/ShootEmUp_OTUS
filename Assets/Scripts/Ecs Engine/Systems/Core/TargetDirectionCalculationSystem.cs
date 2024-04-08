using System;
using EcsEngine.Components;
using EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    public sealed class TargetDirectionCalculationSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Target, MovementDirection, Position>, Exc<Inactive>> _filter;
        private readonly EcsWorldInject _world;
        
        private EcsPool<Target> _targetPool;
        private EcsPool<MovementDirection> _movementDirectionPool;
        private EcsPool<Position> _positionPool;
        
        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _targetPool = _filter.Pools.Inc1;
            _movementDirectionPool = _filter.Pools.Inc2;
            _positionPool = _filter.Pools.Inc3;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int entityId in _filter.Value)
            {
                EcsPackedEntity targetEntity = _targetPool.Get(entityId).Value;
                Vector3 position = _positionPool.Get(entityId).Value;
                ref Vector3 direction = ref _movementDirectionPool.Get(entityId).Value;
                
                if (!targetEntity.Unpack(_world.Value, out int targetEntityId)) continue;
                
                Vector3 targetPosition = _positionPool.Get(targetEntityId).Value;
                
                direction = (targetPosition - position).normalized;
            }
        }
    }
}