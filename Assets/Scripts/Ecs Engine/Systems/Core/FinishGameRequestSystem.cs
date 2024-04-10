using Common;
using EcsEngine.Components.Requests;
using EcsEngine.Extensions;
using GameCycle;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems
{
    public sealed class FinishGameRequestSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<FinishGameRequest>> _filter = EcsWorldsAPI.EventsWorld;
        private readonly EcsWorldInject _world = EcsWorldsAPI.EventsWorld;
        
        private readonly EcsCustomInject<ServiceLocator> _serviceLocator;
        private GameCycleManager _gameCycleManager;

        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _gameCycleManager = _serviceLocator.Value.Resolve<GameCycleManager>();
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int eventId in _filter.Value)
            {
                _gameCycleManager.OnDestroy();
                
                _world.Value.DelEntity(eventId);
            }
        }
    }
}