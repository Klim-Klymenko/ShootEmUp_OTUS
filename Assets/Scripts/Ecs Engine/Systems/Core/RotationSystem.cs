﻿using EcsEngine.Components;
using EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    public sealed class RotationSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Rotation, RotationSpeed, MovementDirection>, Exc<Inactive>> _filter;

        private EcsPool<Rotation> _rotationPool;
        private EcsPool<RotationSpeed> _rotationSpeedPool;
        private EcsPool<MovementDirection> _movementDirectionPool;
        
        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _rotationPool = _filter.Pools.Inc1;
            _rotationSpeedPool = _filter.Pools.Inc2;
            _movementDirectionPool = _filter.Pools.Inc3;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            float deltaTime = Time.deltaTime;
            
            foreach (int entityId in _filter.Value)
            {
                Vector3 direction = _movementDirectionPool.Get(entityId).Value;
                float speed = _rotationSpeedPool.Get(entityId).Value;
                ref Quaternion rotation = ref _rotationPool.Get(entityId).Value;

                if (direction == Vector3.zero) continue;
                
                Quaternion lookDirection = Quaternion.LookRotation(direction);
                Quaternion smoothedRotation = Quaternion.Slerp(rotation, lookDirection, deltaTime * speed);

                rotation = smoothedRotation;
            }
        }
    }
}