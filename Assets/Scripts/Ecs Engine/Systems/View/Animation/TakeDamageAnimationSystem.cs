using System;
using EcsEngine.Components;
using EcsEngine.Components.Events;
using EcsEngine.Components.Tags;
using EcsEngine.Extensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    public sealed class TakeDamageAnimationSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DealDamageEvent, Target>> _filterInject = EcsWorldsAPI.EventsWorld;
        private readonly EcsPoolInject<Movable> _movablePoolInject;
        private readonly EcsPoolInject<UnityAnimator> _animatorPoolInject;
        private readonly EcsPoolInject<TakeDamageAnimation> _damageAnimationPoolInject;
        private readonly EcsPoolInject<Inactive> _inactivePoolInject;
        private readonly EcsWorldInject _worldInject;

        private EcsPool<Target> _targetPool;
        
        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _targetPool = _filterInject.Pools.Inc2;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int eventId in _filterInject.Value)
            {
                EcsPackedEntity targetEntity = _targetPool.Get(eventId).Value;
                
                if (!targetEntity.Unpack(_worldInject.Value, out int targetEntityId)) continue;
                
                if (!_movablePoolInject.Value.Has(targetEntityId)) continue;
                if (_inactivePoolInject.Value.Has(targetEntityId)) continue;
                
                Animator animator = _animatorPoolInject.Value.Get(targetEntityId).Value;
                int damageAnimationHash = _damageAnimationPoolInject.Value.Get(targetEntityId).Value;
                
                animator.SetTrigger(damageAnimationHash);
            }
        }
    }
}