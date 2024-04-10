using EcsEngine.Components;
using EcsEngine.Components.Requests;
using EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems
{
    public sealed class ActiveTargetTrackSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Target>, Exc<Inactive>> _filer;
        private readonly EcsPoolInject<Inactive> _inactivePoolInject;
        private readonly EcsPoolInject<FindTargetRequest> _findTargetRequestPoolInject;
        private readonly EcsWorldInject _world;

        private EcsPool<Target> _targetPool;
        private EcsPool<Inactive> _inactivePool;
        private EcsPool<FindTargetRequest> _findTargetRequestPool;

        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _targetPool = _filer.Pools.Inc1;
            _inactivePool = _inactivePoolInject.Value;
            _findTargetRequestPool = _findTargetRequestPoolInject.Value;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int entityId in _filer.Value)
            {
                EcsPackedEntity targetEntity = _targetPool.Get(entityId).Value;
                
                if (_findTargetRequestPool.Has(entityId)) continue;
                
                if (!targetEntity.Unpack(_world.Value, out int targetEntityId) || _inactivePool.Has(targetEntityId))
                    _findTargetRequestPool.Add(entityId) = new FindTargetRequest();
            }
        }
    }
}