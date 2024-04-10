using EcsEngine.Components;
using EcsEngine.Components.Requests;
using EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    public sealed class AttackAnimationSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AttackRequest, AttackAnimation, UnityAnimator>, Exc<Inactive>> _filterInject;
        
        private EcsPool<AttackRequest> _attackRequestPool;
        private EcsPool<AttackAnimation> _attackAnimationPool;
        private EcsPool<UnityAnimator> _animatorPool;
        
        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _attackRequestPool = _filterInject.Pools.Inc1;
            _attackAnimationPool = _filterInject.Pools.Inc2;
            _animatorPool = _filterInject.Pools.Inc3;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int entityId in _filterInject.Value)
            {
                Animator animator = _animatorPool.Get(entityId).Value;
                int attackAnimationHash = _attackAnimationPool.Get(entityId).Value;
                
                animator.SetTrigger(attackAnimationHash);
                
                _attackRequestPool.Del(entityId);
            }
        }
    }
}