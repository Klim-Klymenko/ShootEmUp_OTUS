using Leopotam.EcsLite;

namespace ECSLite.Extensions.Events
{
    public class AddHereSpecific<T, T1> : IEcsRunSystem
    where T : struct
    where T1 : struct
    {
        private readonly EcsFilter _filter;
        private readonly EcsPool<T> _pool;

        public AddHereSpecific(EcsWorld world) 
        {
            _filter = world.Filter<T1>().End();
            _pool = world.GetPool<T>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entityId in _filter)
            {
                if (!_pool.Has(entityId))
                    _pool.Add(entityId) = new T();
            }
        }
    }
}