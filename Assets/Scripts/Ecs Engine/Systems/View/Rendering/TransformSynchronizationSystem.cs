using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    public sealed class TransformSynchronizationSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Position, Rotation, UnityTransform>> _filterInject;
        
        private EcsPool<Position> _positionPool;
        private EcsPool<Rotation> _rotationPool;
        private EcsPool<UnityTransform> _transformPool;

        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _positionPool = _filterInject.Pools.Inc1;
            _rotationPool = _filterInject.Pools.Inc2;
            _transformPool = _filterInject.Pools.Inc3;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int entityId in _filterInject.Value)
            {
                Vector3 position = _positionPool.Get(entityId).Value;
                Quaternion rotation = _rotationPool.Get(entityId).Value;
                Transform transform = _transformPool.Get(entityId).Value;
                
                transform.position = position;
                transform.rotation = rotation;
            }
        }
    }
}