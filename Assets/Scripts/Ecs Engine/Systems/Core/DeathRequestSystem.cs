using EcsEngine.Components.Events;
using EcsEngine.Components.Requests;
using EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems
{
    public sealed class DeathRequestSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DeathRequest>, Exc<Inactive>> _filterInject;
        private readonly EcsPoolInject<DeathEvent> _deathEventPoolInject;
        private readonly EcsPoolInject<Inactive> _inactivePoolInject;
        
        private EcsPool<DeathRequest> _deathRequestPool;
        
        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _deathRequestPool = _filterInject.Pools.Inc1;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int entityId in _filterInject.Value)
            {
                _inactivePoolInject.Value.Add(entityId) = new Inactive();
                _deathEventPoolInject.Value.Add(entityId) = new DeathEvent();
                
                _deathRequestPool.Del(entityId);
            }
        }
    }
}