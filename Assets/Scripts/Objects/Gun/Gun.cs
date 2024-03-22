using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using Common;
using GameCycle;
using GameEngine;
using UnityEngine;
using Zenject;

namespace Objects
{
    internal sealed class Gun : AtomicObject, IInitializeGameListener, IUpdateGameListener, IFinishGameListener
    {
        [Section]
        [SerializeField]
        private SwitchGameObjectComponent _switchGameObjectComponent;

        [Section]
        [SerializeField] 
        private ShootComponent _shootComponent;
        
        [SerializeField]
        private ReplenishComponent _replenishComponent;
        
        private readonly AndExpression _aliveCondition = new();
        
        private ISpawner<Transform> _bulletSpawner;
        
        public IAtomicObservable ShootObservable => _shootComponent.ShootObservable;
        
        [Inject]
        internal void Construct(ISpawner<Transform> bulletSpawner)
        {
            _bulletSpawner = bulletSpawner;
        }
        
        public override void Compose()
        {
            base.Compose();
            
            ISpawner<Transform> bulletSpawner = _bulletSpawner;
            _aliveCondition.Append(true.AsValue());
            
            _switchGameObjectComponent.Compose(gameObject);

            _shootComponent.Let(it =>
            {
                it.Compose(bulletSpawner);
                it.ShootCondition.Append(_aliveCondition);
            });

            _replenishComponent.Let(it =>
            {
                it.Compose(_shootComponent.Charges);
                it.ReplenishCondition.Append(_aliveCondition);
            });
            
            _switchGameObjectComponent.OnEnable();
            _replenishComponent.OnEnable();
        }

        void IInitializeGameListener.OnInitialize()
        {
            Compose();
        }

        void IUpdateGameListener.OnUpdate()
        {
            _replenishComponent.Update();
        }

        void IFinishGameListener.OnFinish()
        {
            _aliveCondition.Append(false.AsValue());
            
            _switchGameObjectComponent.OnDisable();
            _replenishComponent.OnDisable();
            
            _switchGameObjectComponent.Dispose();
            _shootComponent.Dispose();
            _replenishComponent.Dispose();
        }
    }
}