using EcsEngine.Components;
using EcsEngine.Components.Requests;
using EcsEngine.Components.Tags;
using EcsEngine.Extensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems
{
    public sealed class CollisionRequestSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CollisionRequest, Source, Target>, Exc<Inactive>> _filterInject = EcsWorldsAPI.EventsWorld;
        private readonly EcsPoolInject<DealDamageRequest> _dealDamageRequestPoolInject = EcsWorldsAPI.EventsWorld;
        private readonly EcsWorldInject _eventsWorldInject = EcsWorldsAPI.EventsWorld;

        private readonly EcsWorldInject _gameObjectsWorldInject;
        private readonly EcsPoolInject<Inactive> _inactivePoolInject;
        private readonly EcsPoolInject<Attackable> _attackablePoolInject;
        private readonly EcsPoolInject<ProjectileTag> _projectileTagPoolInject;
        private readonly EcsPoolInject<DeathRequest> _deathRequestPoolInject;

        private EcsPool<Source> _sourcePool;
        private EcsPool<Target> _targetPool;
        
        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _sourcePool = _filterInject.Pools.Inc2;
            _targetPool = _filterInject.Pools.Inc3;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int eventId in _filterInject.Value)
            {
                Source source = _sourcePool.Get(eventId);
                Target target = _targetPool.Get(eventId);
                
                if (!source.Value.Unpack(_gameObjectsWorldInject.Value, out int sourceEntityId)) continue;
                if (!target.Value.Unpack(_gameObjectsWorldInject.Value, out int targetEntityId)) continue;
                
                if (_inactivePoolInject.Value.Has(sourceEntityId) || _inactivePoolInject.Value.Has(targetEntityId)) continue;
                
                if (_projectileTagPoolInject.Value.Has(sourceEntityId) && !_deathRequestPoolInject.Value.Has(sourceEntityId))
                    _deathRequestPoolInject.Value.Add(sourceEntityId) = new DeathRequest();
                
                if (!_attackablePoolInject.Value.Has(targetEntityId)) continue;
                
                int dealDamageRequestId = _eventsWorldInject.Value.NewEntity();

                _sourcePool.Add(dealDamageRequestId) = source;
                _targetPool.Add(dealDamageRequestId) = target;
                _dealDamageRequestPoolInject.Value.Add(dealDamageRequestId) = new DealDamageRequest();

                _eventsWorldInject.Value.DelEntity(eventId);
            }
        }
    }
}