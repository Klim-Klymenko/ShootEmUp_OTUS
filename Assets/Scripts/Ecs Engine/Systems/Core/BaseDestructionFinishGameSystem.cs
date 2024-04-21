using EcsEngine.Components.Events;
using EcsEngine.Components.Requests;
using EcsEngine.Components.Tags;
using EcsEngine.Extensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    public sealed class BaseDestructionFinishGameSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<BaseTag, DeathEvent>> _filterInject;
        
        private readonly EcsWorldInject _worldInject = EcsWorldsAPI.EventsWorld;
        private readonly EcsPoolInject<FinishGameRequest> _finishRequestPoolInject = EcsWorldsAPI.EventsWorld;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int entityId in _filterInject.Value)
            {
                int finishRequestId = _worldInject.Value.NewEntity();
                _finishRequestPoolInject.Value.Add(finishRequestId) = new FinishGameRequest();
                
                return;
            }
        }
    }
}