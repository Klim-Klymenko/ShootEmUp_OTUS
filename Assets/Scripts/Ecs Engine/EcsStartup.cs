using EcsEngine.Components;
using EcsEngine.Components.Events;
using EcsEngine.Components.Requests;
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
using Zenject;

namespace EcsEngine
{ 
    [UsedImplicitly]
    internal sealed class EcsStartup : IInitializeGameListener, IStartGameListener, IUpdateGameListener, IFinishGameListener
    {
        private EcsWorld _gameObjectsWorld;    
        private EcsWorld _eventsWorld;
        private IEcsSystems _systems;

        private readonly EntityManager _entityManager;
        private readonly DiContainer _diContainer;
        private readonly EcsEntityBuilder _entityBuilder;

        internal EcsStartup(EntityManager entityManager, DiContainer diContainer, EcsEntityBuilder entityBuilder)
        {
            _entityManager = entityManager;
            _diContainer = diContainer;
            _entityBuilder = entityBuilder;
        }
        
        void IInitializeGameListener.OnInitialize()
        {
            _gameObjectsWorld = new EcsWorld();
            _eventsWorld = new EcsWorld();
            _systems = new EcsSystems(_gameObjectsWorld, _diContainer);

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
                
                .Add(new FinishGameRequestSystem())
                .Add(new AnimatorDisableSystem())
                .Add(new FinishGameSystem())
                
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
                .Add(new BaseDestructionFinishGameSystem())
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
                
                .OneFrame<CollisionRequest>(EcsWorldsAPI.EventsWorld)
                .OneFrame<DealDamageEvent>(EcsWorldsAPI.EventsWorld)
                .OneFrame<FinishGameEvent>(EcsWorldsAPI.EventsWorld);
        }

        private void AddExtraWorlds()
        {
            _systems.AddWorld(_eventsWorld, EcsWorldsAPI.EventsWorld);
        }

        private void AddInjections()
        {
            _systems.Inject(_diContainer);
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