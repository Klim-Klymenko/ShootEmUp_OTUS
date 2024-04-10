using Common;
using EcsEngine.Components;
using EcsEngine.Components.Events;
using EcsEngine.Extensions;
using EcsEngine.Systems;
using EcsEngine.Systems.Sound;
using EcsEngine.Systems.View.Particle;
using ECSLite.Extensions.Events;
using GameCycle;
using JetBrains.Annotations;
using Leopotam.EcsLite.UnityEditor;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;

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

            AddExtraWorlds();
            AddSystems();
            AddInjections();
            
            _entityBuilder.Construct(_eventsWorld);
            _entityManager.Initialize(_gameObjectsWorld);
        }

        private void AddSystems()
        {
            _systems
                .Add(new CollisionRequestSystem())
                
                .Add(new FinishGameTrackSystem())
                .Add(new FinishGameRequestSystem())
                .Add(new ActiveTargetTrackSystem())
                .Add(new ClosestTargetSearchRequestSystem())
                .Add(new TargetDirectionCalculationSystem())
                .Add(new MovementSystem())
                .Add(new RotationSystem())
                .Add(new AttackTrackSystem())
                .Add(new MoveStateControlSystem())
                .Add(new CooldownAttackControlSystem())
                .Add(new TimerSystem())

                .Add(new AttackEventSystem())
                .Add(new HitRequestSystem())
                .Add(new DealDamageRequestSystem())
                .Add(new ShootRequestSystem())
                .Add(new SpawnRequestSystem())
                .Add(new ProjectileFactoryRequestSystem())
                
                .Add(new DeathTrackSystem())
                .Add(new DeathRequestSystem())
                .Add(new DeadDestructionSystem())

                .Add(new TransformSynchronizationSystem())
                
                .Add(new MovementAnimationSystem())
                .Add(new AttackAnimationSystem())
                .Add(new AttackSoundSystem())
                .Add(new AttackParticleSystem())
                .Add(new TakeDamageAnimationSystem())
                .Add(new TakeDamageSoundSystem())
                .Add(new TakeDamageParticleSystem())

                .Add(new EcsWorldDebugSystem())
                .Add(new EcsWorldDebugSystem(EcsWorldsAPI.EventsWorld))

                .DelHere<AttackEvent>()
                .DelHere<DeathEvent>()
                .OneFrame<DealDamageEvent>(EcsWorldsAPI.EventsWorld)
                .OneFrame<CollisionRequest>(EcsWorldsAPI.EventsWorld);
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

            if (_systems != null)
                _systems = null;
        }
    }
}