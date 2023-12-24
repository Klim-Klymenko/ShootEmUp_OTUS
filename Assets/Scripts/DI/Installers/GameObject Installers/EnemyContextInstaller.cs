using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(EnemyReferenceComponent))]
    [RequireComponent(typeof(MoveComponent))]
    [RequireComponent(typeof(HitPointsComponent))]
    public sealed class EnemyContextInstaller : DependencyInstaller
    {
        [SerializeField]
        private HitPointsComponent _hitPointsComponent;

        [SerializeField]
        private EnemyReferenceComponent _referenceComponent;

        [SerializeField, Service]
        private Transform _transform;

        [SerializeField, Service] 
        private MoveComponent _moveComponent;

        [Service, Listener]
        private readonly EnemyMoveAgent _moveAgent = new();

        [Service]
        private readonly EnemyAttackAgent _attackAgent = new();
        
        [Listener]
        private EnemyAttackController _attackController;

        [Listener]
        private EnemyDeathObserver _deathObserver;

        private void OnValidate()
        {
            _transform = transform;
            _referenceComponent = GetComponent<EnemyReferenceComponent>();
            _hitPointsComponent = GetComponent<HitPointsComponent>();
            _moveComponent = GetComponent<MoveComponent>();
        }

        public IEnumerable<object> ProvideInjectablesWithSceneDependencies()
        {
            yield return CreateOrProvideAttackController();
            yield return CreateOrProvideDeathObserver();
        }
        
        public override IEnumerable<object> ProvideInjectables()
        {
            yield return _referenceComponent;
            yield return _moveAgent;
        }

        public override IEnumerable<IGameListener> ProvideGameListeners()
        {
            yield return _moveAgent;
            yield return CreateOrProvideAttackController();
            yield return CreateOrProvideDeathObserver();
        }
        
        private EnemyAttackController CreateOrProvideAttackController() =>
            _attackController ??= new EnemyAttackController(_hitPointsComponent, _moveAgent, _moveComponent, _attackAgent);
        
        private EnemyDeathObserver CreateOrProvideDeathObserver() =>
            _deathObserver ??= new EnemyDeathObserver(_hitPointsComponent, _referenceComponent);
    }
}
