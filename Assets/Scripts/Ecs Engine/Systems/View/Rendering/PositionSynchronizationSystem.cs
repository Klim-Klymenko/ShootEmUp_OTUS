using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    public sealed class PositionSynchronizationSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Position, UnityTransform>> _filter;
        
        private EcsPool<Position> _positionPool;
        private EcsPool<UnityTransform> _transformPool;

        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _positionPool = _filter.Pools.Inc1;
            _transformPool = _filter.Pools.Inc2;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int entityId in _filter.Value)
            {
                Vector3 position = _positionPool.Get(entityId).Value;
                Transform transform = _transformPool.Get(entityId).Value;
                
                transform.position = position;
            }
        }
    }
}