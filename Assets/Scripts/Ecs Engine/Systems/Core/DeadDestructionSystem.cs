using EcsEngine.Components.Events;
using EcsEngine.Extensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems
{
    public sealed class DeadDestructionSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DeathEvent>> _filterInject;
        private readonly EcsWorldInject _worldInject;
        
        private readonly EcsZenject<EntityManager> _entityManagerInject;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int entityId in _filterInject.Value)
            {
                _entityManagerInject.Value.Destroy(entityId);
            }
        }
    }
}