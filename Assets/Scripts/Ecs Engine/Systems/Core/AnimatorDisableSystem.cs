using EcsEngine.Components;
using EcsEngine.Components.Requests;
using EcsEngine.Components.Tags;
using EcsEngine.Extensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    public sealed class AnimatorDisableSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<FinishGameEvent>> _eventFilterInject = EcsWorldsAPI.EventsWorld;
        private readonly EcsFilterInject<Inc<UnityAnimator>, Exc<Inactive>> _animatorFilterInject;

        private EcsPool<UnityAnimator> _animatorPool;
        
        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _animatorPool = _animatorFilterInject.Pools.Inc1;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int eventId in _eventFilterInject.Value)
            {
                foreach (int entityId in _animatorFilterInject.Value)
                {
                    Animator animator = _animatorPool.Get(entityId).Value;

                    animator.enabled = false;
                }
            }
        }
    }
}