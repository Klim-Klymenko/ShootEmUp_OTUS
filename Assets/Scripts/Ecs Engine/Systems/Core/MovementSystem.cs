using EcsEngine.Components;
using EcsEngine.Components.Events;
using EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    public sealed class MovementSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MovementDirection, MovementSpeed, Position>, Exc<Inactive, AttackEnabled>> _filter;

        private EcsPool<MovementDirection> _movementDirectionPool;
        private EcsPool<MovementSpeed> _movementSpeedPool;
        private EcsPool<Position> _positionPool;
        
        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _movementDirectionPool = _filter.Pools.Inc1;
            _movementSpeedPool = _filter.Pools.Inc2;
            _positionPool = _filter.Pools.Inc3;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            float deltaTime = Time.deltaTime;
            
            foreach (int entityId in _filter.Value)
            {
                Vector3 direction = _movementDirectionPool.Get(entityId).Value;
                float speed = _movementSpeedPool.Get(entityId).Value;
                ref Vector3 position = ref _positionPool.Get(entityId).Value;
                
                position += direction * (speed * deltaTime);
            }
        }
    }
}