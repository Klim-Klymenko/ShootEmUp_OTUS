using EcsEngine.Components.Events;
using EcsEngine.Components.Requests;
using EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems
{
    public sealed class DeathRequestSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DeathRequest>, Exc<Inactive>> _filter;
        private readonly EcsPoolInject<DeathEvent> _deathEventPoolInject;
        private readonly EcsPoolInject<Inactive> _inactivePoolInject;
        
        private EcsPool<DeathRequest> _deathRequestPool;
        private EcsPool<DeathEvent> _deathEventPool;
        private EcsPool<Inactive> _inactivePool;
        
        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _deathRequestPool = _filter.Pools.Inc1;
            _deathEventPool = _deathEventPoolInject.Value;
            _inactivePool = _inactivePoolInject.Value;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int entityId in _filter.Value)
            {
                _inactivePool.Add(entityId) = new Inactive();
                _deathEventPool.Add(entityId) = new DeathEvent();
                
                _deathRequestPool.Del(entityId);
            }
        }
    }
}