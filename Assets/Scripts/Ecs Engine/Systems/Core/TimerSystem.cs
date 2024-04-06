using EcsEngine.Components;
using EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    public sealed class TimerSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Timer, Tickable>> _filter;
        
        private EcsPool<Timer> _timerPool;
        private EcsPool<Tickable> _tickablePool;
        
        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _timerPool = _filter.Pools.Inc1;
            _tickablePool = _filter.Pools.Inc2;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            float deltaTime = Time.deltaTime;
            
            foreach (int entityId in _filter.Value)
            {
                ref Timer timer = ref _timerPool.Get(entityId);
                
                ref float currentTime = ref timer.CurrentTime;
                float endTime = timer.EndTime;
                float duration = timer.Duration;
                
                if (currentTime <= endTime)
                {
                    currentTime = duration;
                    _tickablePool.Del(entityId);
                    continue;
                }

                currentTime -= deltaTime;
            }
        }
    }
}