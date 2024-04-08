using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    public sealed class RotationSynchronizationSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Rotation, UnityTransform>> _filter;
        
        private EcsPool<Rotation> _rotationPool;
        private EcsPool<UnityTransform> _transformPool;

        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _rotationPool = _filter.Pools.Inc1;
            _transformPool = _filter.Pools.Inc2;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int entityId in _filter.Value)
            {
                Quaternion rotation = _rotationPool.Get(entityId).Value;
                Transform transform = _transformPool.Get(entityId).Value;
                
                transform.rotation = rotation;
            }
        }
    }
}