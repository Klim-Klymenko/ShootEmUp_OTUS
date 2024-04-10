using EcsEngine.Components;
using EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    public sealed class TargetDirectionCalculationSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Target, MovementDirection, Position>, Exc<Inactive>> _filterInject;
        private readonly EcsWorldInject _worldInject;
        
        private EcsPool<Target> _targetPool;
        private EcsPool<MovementDirection> _movementDirectionPool;
        private EcsPool<Position> _positionPool;
        
        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _targetPool = _filterInject.Pools.Inc1;
            _movementDirectionPool = _filterInject.Pools.Inc2;
            _positionPool = _filterInject.Pools.Inc3;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int entityId in _filterInject.Value)
            {
                EcsPackedEntity targetEntity = _targetPool.Get(entityId).Value;
                Vector3 position = _positionPool.Get(entityId).Value;
                ref Vector3 direction = ref _movementDirectionPool.Get(entityId).Value;
                
                if (!targetEntity.Unpack(_worldInject.Value, out int targetEntityId)) continue;
                
                Vector3 targetPosition = _positionPool.Get(targetEntityId).Value;
                
                direction = (targetPosition - position).normalized;
            }
        }
    }
}