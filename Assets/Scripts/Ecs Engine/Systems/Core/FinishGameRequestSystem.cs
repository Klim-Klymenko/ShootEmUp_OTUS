using EcsEngine.Components.Requests;
using EcsEngine.Extensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems
{ 
    public sealed class FinishGameRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<FinishGameRequest>> _filterInject = EcsWorldsAPI.EventsWorld;
        private readonly EcsPoolInject<FinishGameEvent> _finishGameEventPoolInject = EcsWorldsAPI.EventsWorld;
        private readonly EcsWorldInject _worldInject = EcsWorldsAPI.EventsWorld;
        
        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int eventId in _filterInject.Value)
            {
                int finishEventId = _worldInject.Value.NewEntity();
                _finishGameEventPoolInject.Value.Add(finishEventId) = new FinishGameEvent();
                
                _worldInject.Value.DelEntity(eventId);
            }
        }
    }
}