using EcsEngine.Components;
using EcsEngine.Components.Requests;
using EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems
{
    public sealed class ActiveTargetTrackSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Target>, Exc<Inactive, TargetSearchDisabled>> _filerInject;
        private readonly EcsPoolInject<Inactive> _inactivePoolInject;
        private readonly EcsPoolInject<FindTargetRequest> _findTargetRequestPoolInject;
        private readonly EcsWorldInject _worldInject;

        private EcsPool<Target> _targetPool;

        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _targetPool = _filerInject.Pools.Inc1;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int entityId in _filerInject.Value)
            {
                EcsPackedEntity targetEntity = _targetPool.Get(entityId).Value;
                
                if (_findTargetRequestPoolInject.Value.Has(entityId)) continue;
                
                if (!targetEntity.Unpack(_worldInject.Value, out int targetEntityId) || _inactivePoolInject.Value.Has(targetEntityId))
                    _findTargetRequestPoolInject.Value.Add(entityId) = new FindTargetRequest();
            }
        }
    }
}