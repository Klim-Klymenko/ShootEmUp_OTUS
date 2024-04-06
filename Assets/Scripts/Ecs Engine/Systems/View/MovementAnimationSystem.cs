using EcsEngine.Components;
using EcsEngine.Components.Tags;
using EcsEngine.Data;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems
{
    public sealed class MovementAnimationSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<UnityAnimator, MoveAnimation, UnitState>, Exc<Inactive>> _filter;

        private EcsPool<UnityAnimator> _animatorPool;
        private EcsPool<MoveAnimation> _moveAnimationPool;
        private EcsPool<UnitState> _moveStatePool;
        
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
                UnitState unitState = _moveStatePool.Get(entityId);
                
                if (unitState.Value is State.Idle or State.Move) 
                    animator.Value.SetInteger(moveAnimation.Value, unitState.Hash);
            }
        }
    }
}