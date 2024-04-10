using EcsEngine.Components;
using EcsEngine.Components.Tags;
using EcsEngine.Data;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems
{
    public sealed class MoveStateControlSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MoveState>, Exc<Inactive>> _filterInject;
        private readonly EcsPoolInject<AttackEnabled> _attackEnabledPoolInject;

        private EcsPool<MoveState> _moveStatePool;

        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _moveStatePool = _filterInject.Pools.Inc1;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int entityId in _filterInject.Value)
            {
                ref State moveState = ref _moveStatePool.Get(entityId).Value;

                if (_attackEnabledPoolInject.Value.Has(entityId))
                {
                    moveState = State.Idle;
                    continue;
                }
                
                moveState = State.Move;
            }
        }
    }
}