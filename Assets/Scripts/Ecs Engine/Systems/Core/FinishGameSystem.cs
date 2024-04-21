using EcsEngine.Components.Requests;
using EcsEngine.Extensions;
using GameCycle;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems
{
    public sealed class FinishGameSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<FinishGameEvent>> _filterInject = EcsWorldsAPI.EventsWorld;
        
        private readonly EcsZenject<GameCycleManager> _gameCycleManagerInject;
        
        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int eventId in _filterInject.Value)
            {
                _gameCycleManagerInject.Value.OnDestroy();
            }
        }
    }
}