using JetBrains.Annotations;
using Leopotam.EcsLite;

namespace EcsEngine.Extensions
{
    [UsedImplicitly]
    public sealed class EcsEntityBuilder
    {
        private EcsWorld _world;
        private int _entityId;

        public void Construct(EcsWorld world)
        {
            _world = world;
        }
        
        public EcsEntityBuilder CreateEntity()
        {
            _entityId = _world.NewEntity();
            
            return this;
        }

        public EcsEntityBuilder Add<T>(T component) where T : struct
        {
            EcsPool<T> pool = _world.GetPool<T>();
            pool.Add(_entityId) = component;
            
            return this;
        }
    }
}