using Leopotam.EcsLite;

namespace ECSLite.Extensions.Events
{
    public sealed class DelHereSpecificSystem<T, T1> : IEcsRunSystem
        where T : struct
        where T1 : struct 
    {
        private readonly EcsFilter _filter;
        private readonly EcsPool<T> _pool;

        public DelHereSpecificSystem(EcsWorld world) 
        {
            _filter = world.Filter<T>().Inc<T1>().End();
            
            _pool = world.GetPool<T>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entityId in _filter)
            {
                _pool.Del(entityId);
            }
        }
    }
}