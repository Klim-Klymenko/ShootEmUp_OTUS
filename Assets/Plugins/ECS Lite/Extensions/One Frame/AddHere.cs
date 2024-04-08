using Leopotam.EcsLite;

namespace ECSLite.Extensions.Events
{
    public class AddHere<T> : IEcsRunSystem
    where T : struct
    {
        private readonly EcsFilter _filter;
        private readonly EcsPool<T> _pool;

        public AddHere(EcsWorld world) 
        {
            _filter = world.Filter<T>().End();
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