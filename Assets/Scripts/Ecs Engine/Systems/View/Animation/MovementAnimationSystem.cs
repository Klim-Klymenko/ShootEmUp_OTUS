using EcsEngine.Components;
using EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems
{
    public sealed class MovementAnimationSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<UnityAnimator, MoveAnimation, MoveState>, Exc<Inactive>> _filter;

        private EcsPool<UnityAnimator> _animatorPool;
        private EcsPool<MoveAnimation> _moveAnimationPool;
        private EcsPool<MoveState> _moveStatePool;
        
        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _animatorPool = _filter.Pools.Inc1;
            _moveAnimationPool = _filter.Pools.Inc2;
            _moveStatePool = _filter.Pools.Inc3;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int entityId in _filter.Value)
            {
                UnityAnimator animator = _animatorPool.Get(entityId);
                MoveAnimation moveAnimation = _moveAnimationPool.Get(entityId);
                MoveState moveState = _moveStatePool.Get(entityId);
                
                animator.Value.SetInteger(moveAnimation.Value, moveState.Hash);
            }
        }
    }
}