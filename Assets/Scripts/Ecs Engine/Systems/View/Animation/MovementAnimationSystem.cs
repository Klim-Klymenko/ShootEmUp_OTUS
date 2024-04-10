using EcsEngine.Components;
using EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    public sealed class MovementAnimationSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<UnityAnimator, MoveAnimation, MoveState>, Exc<Inactive>> _filterInject;

        private EcsPool<UnityAnimator> _animatorPool;
        private EcsPool<MoveAnimation> _moveAnimationPool;
        private EcsPool<MoveState> _moveStatePool;
        
        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _animatorPool = _filterInject.Pools.Inc1;
            _moveAnimationPool = _filterInject.Pools.Inc2;
            _moveStatePool = _filterInject.Pools.Inc3;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int entityId in _filterInject.Value)
            {
                Animator animator = _animatorPool.Get(entityId).Value;
                int moveAnimation = _moveAnimationPool.Get(entityId).Value;
                int movementAnimationHash = _moveStatePool.Get(entityId).Hash;
                
                animator.SetInteger(moveAnimation, movementAnimationHash);
            }
        }
    }
}