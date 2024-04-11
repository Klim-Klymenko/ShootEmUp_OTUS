using Common;
using EcsEngine.Components.Requests;
using EcsEngine.Extensions;
using GameCycle;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems
{
    public sealed class FinishGameSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<FinishGameEvent>> _filterInject = EcsWorldsAPI.EventsWorld;
        
        private readonly EcsCustomInject<ServiceLocator> _serviceLocatorInject;
        private GameCycleManager _gameCycleManager;

        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _gameCycleManager = _serviceLocatorInject.Value.Resolve<GameCycleManager>();
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int eventId in _filterInject.Value)
            {
                _gameCycleManager.OnDestroy();
            }
        }
    }
}