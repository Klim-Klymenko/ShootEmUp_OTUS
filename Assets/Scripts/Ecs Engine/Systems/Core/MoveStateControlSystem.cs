using EcsEngine.Components;
using EcsEngine.Components.Tags;
using EcsEngine.Data;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems
{
    public sealed class MoveStateControlSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MoveState>, Exc<Inactive>> _filter;
        private readonly EcsPoolInject<AttackEnabled> _attackEnabledPoolInject;

        private EcsPool<MoveState> _moveStatePool;
        private EcsPool<AttackEnabled> _attackEnabledPool;

        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _moveStatePool = _filter.Pools.Inc1;
            _attackEnabledPool = _attackEnabledPoolInject.Value;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int entityId in _filter.Value)
            {
                ref State moveState = ref _moveStatePool.Get(entityId).Value;

                if (_attackEnabledPool.Has(entityId))
                {
                    moveState = State.Idle;
                    continue;
                }
                
                moveState = State.Move;
            }
        }
    }
}