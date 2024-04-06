using Common;
using EcsEngine.Components.Tags;
using EcsEngine.Extensions;
using EcsEngine.Systems;
using GameCycle;
using JetBrains.Annotations;
using Leopotam.EcsLite.UnityEditor;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Debug = UnityEngine.Debug;

namespace EcsEngine
{ 
    [UsedImplicitly]
    internal sealed class EcsStartup : IInitializeGameListener, IStartGameListener, IUpdateGameListener, IFinishGameListener
    {
        private EcsWorld _gameObjectsWorld;    
        private EcsWorld _eventsWorld;
        private IEcsSystems _systems;

        private readonly EntityManager _entityManager;
        private readonly ServiceLocator _serviceLocator;
        private readonly EcsEntityBuilder _entityBuilder;

        internal EcsStartup(EntityManager entityManager, ServiceLocator serviceLocator, EcsEntityBuilder entityBuilder)
        {
            _entityManager = entityManager;
            _serviceLocator = serviceLocator;
            _entityBuilder = entityBuilder;
        }
        
        void IInitializeGameListener.OnInitialize()
        {
            _gameObjectsWorld = new EcsWorld();
            _eventsWorld = new EcsWorld();
            _systems = new EcsSystems(_gameObjectsWorld);
            
            AddSystems();
            AddExtraWorlds();
            AddInjections();
            
            _entityBuilder.Construct(_eventsWorld);
            _entityManager.Initialize(_gameObjectsWorld);
        }

        private void AddSystems()
        {
            _systems
                .Add(new TargetControlSystem())
                
                .Add(new ClosestTargetSearchSystem())
                .Add(new AttackControlSystem())
                .Add(new AttackRequestSystem())
                .Add(new TimerSystem())
                .Add(new TargetDirectionCalculationSystem())
                .Add(new MovementSystem())
                .Add(new RotationSystem())
                
                .Add(new TransformSynchronizationSystem())
                .Add(new MovementAnimationSystem())
                .Add(new AttackAnimationSystem())
                
                .Add(new EcsWorldDebugSystem())
                .Add(new EcsWorldDebugSystem(EcsWorldsAPI.EventsWorld));
        }

        private void AddExtraWorlds()
        {
            _systems.AddWorld(_eventsWorld, EcsWorldsAPI.EventsWorld);
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
                _systems.Destroy();
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