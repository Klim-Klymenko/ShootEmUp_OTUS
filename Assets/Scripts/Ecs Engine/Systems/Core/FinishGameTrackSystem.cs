using EcsEngine.Components;
using EcsEngine.Components.Requests;
using EcsEngine.Components.Tags;
using EcsEngine.Extensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    public sealed class FinishGameTrackSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<BaseTag, BaseInsufficientAmount, TeamAffiliation>, Exc<Inactive>> _filter;
        
        private EcsPool<BaseInsufficientAmount> _insufficientAmountPool;
        
        private readonly EcsWorldInject _world = EcsWorldsAPI.EventsWorld;
        private readonly EcsPoolInject<FinishGameRequest> _finishRequestPoolInject = EcsWorldsAPI.EventsWorld;


        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _insufficientAmountPool = _filter.Pools.Inc2;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            int entitiesCount = _filter.Value.GetEntitiesCount();
            
            foreach (int entityId in _filter.Value)
            {
                int baseInsufficientAmount = _insufficientAmountPool.Get(entityId).Value;
                
                if (entitiesCount > baseInsufficientAmount) continue;
                 
                int finishRequestId = _world.Value.NewEntity();
                _finishRequestPoolInject.Value.Add(finishRequestId) = new FinishGameRequest();
                
                return;
            }
        }
    }
}