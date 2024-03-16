using Common;
using EcsEngine.World;
using GameCycle;
using JetBrains.Annotations;
using Leopotam.EcsLite.UnityEditor;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine
{ 
    [UsedImplicitly]
    internal sealed class EcsStartup : IInitializeGameListener, IStartGameListener, IUpdateGameListener, IFinishGameListener
    {
        private EcsWorld _gameObjectsWorld;    
        private EcsWorld _eventsWorld;
        private IEcsSystems _systems;

        private readonly ServiceLocator _serviceLocator;
        
        public EcsStartup(ServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }
        
        void IInitializeGameListener.OnInitialize()
        {
            _gameObjectsWorld = new EcsWorld();
            _eventsWorld = new EcsWorld();
            _systems = new EcsSystems(_gameObjectsWorld);
            
            AddSystems();
            AddExtraWorlds();
            AddInjections();
        }

        private void AddSystems()
        {
            _systems
                .Add(new EcsWorldDebugSystem())
                .Add(new EcsWorldDebugSystem(WorldsAPI.EventsWorld));
        }

        private void AddExtraWorlds()
        {
            _systems.AddWorld(_eventsWorld, WorldsAPI.EventsWorld);
        }

        private void AddInjections()
        {
            _systems.Inject(_serviceLocator);
        }
        
        void IStartGameListener.OnStart()
        {
            _systems.Init();
        }

        void IUpdateGameListener.OnUpdate()
        {
            _systems.Run();
        }

        void IFinishGameListener.OnFinish()
        {
            if (_systems != null) 
            {
                _systems.Destroy ();
                _systems = null;
            }
            
            if (_eventsWorld != null) 
            {
                _eventsWorld.Destroy();
                _eventsWorld = null;
            }
            
            if (_gameObjectsWorld != null) 
            {
                _gameObjectsWorld.Destroy();
                _gameObjectsWorld = null;
            }
        }
    }
}