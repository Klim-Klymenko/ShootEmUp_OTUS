using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(EnemyReferenceComponent))]
    public sealed class EnemyInstaller : Installer
    {
        [SerializeField]
        private EnemyReferenceComponent _referenceComponent;

        [SerializeField, Service]
        private EnemyService _enemyService;
        
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
            _referenceComponent = GetComponent<EnemyReferenceComponent>();
        }

        public override IEnumerable<object> ProvideServices()
        {
            yield return _enemyService;
            yield return _moveAgent;
            yield return _attackAgent;
            yield return CombineListeners();
        }
        
        public override IEnumerable<object> ProvideInjectables()
        {
            yield return _referenceComponent;
            yield return _moveAgent;
            yield return CreateOrProvideAttackController();
            yield return CreateOrProvideDeathObserver();
        }

        public override IEnumerable<IGameListener> ProvideGameListeners()
        {
            yield return _moveAgent;
            yield return CreateOrProvideAttackController();
            yield return CreateOrProvideDeathObserver();
        }
        
        private EnemyAttackController CreateOrProvideAttackController() =>
            _attackController ??= new EnemyAttackController(_enemyService, _moveAgent, _attackAgent);
        
        private EnemyDeathObserver CreateOrProvideDeathObserver() =>
            _deathObserver ??= new EnemyDeathObserver(_enemyService, _referenceComponent);

        private IEnumerable<IGameListener> CombineListeners() => new IGameListener[]
        {
            _moveAgent, CreateOrProvideAttackController(), CreateOrProvideDeathObserver()
        };
    }
}
