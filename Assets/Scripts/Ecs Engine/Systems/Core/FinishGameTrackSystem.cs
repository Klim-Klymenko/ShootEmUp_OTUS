using EcsEngine.Components;
using EcsEngine.Components.Requests;
using EcsEngine.Components.Tags;
using EcsEngine.Extensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems
{
    public sealed class FinishGameTrackSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<BaseTag, BaseInsufficientAmount, TeamAffiliation>, Exc<Inactive>> _filterInject;
        
        private readonly EcsWorldInject _worldInject = EcsWorldsAPI.EventsWorld;
        private readonly EcsPoolInject<FinishGameEvent> _finishEventPoolInject = EcsWorldsAPI.EventsWorld;

        private EcsPool<BaseInsufficientAmount> _insufficientAmountPool;

        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _insufficientAmountPool = _filterInject.Pools.Inc2;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            int entitiesCount = _filterInject.Value.GetEntitiesCount();
            
            foreach (int entityId in _filterInject.Value)
            {
                int baseInsufficientAmount = _insufficientAmountPool.Get(entityId).Value;
                
                if (entitiesCount > baseInsufficientAmount) continue;
                 
                int finishEventId = _worldInject.Value.NewEntity();
                _finishEventPoolInject.Value.Add(finishEventId) = new FinishGameEvent();
                
                return;
            }
        }
    }
}