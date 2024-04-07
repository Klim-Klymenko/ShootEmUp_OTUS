using Leopotam.EcsLite;

namespace ECSLite.Extensions.Events
{
    public sealed class OneFrameSystem<T> : IEcsRunSystem where T : struct 
    {
        private readonly EcsFilter _filter;
        private readonly EcsWorld _world;

        public OneFrameSystem(EcsWorld world)
        {
            _filter = world.Filter<T>().End();
            _world = world;
        }

        public void Run (IEcsSystems systems)
        {
            foreach (int entityId in _filter) 
                _world.DelEntity(entityId);
        }
    }
}