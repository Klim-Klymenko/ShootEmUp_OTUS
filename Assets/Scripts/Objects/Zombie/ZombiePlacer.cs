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
    internal sealed class ZombiePlacer : AtomicObject, IStartGameListener, IUpdateGameListener, IFinishGameListener
    {
        [SerializeField]
        [HideInInspector]
        private AtomicAction _zombieSpawnAction;
        
        private readonly AtomicVariable<bool> _aliveCondition = new();

        [SerializeField]
        private CooldownComponent _cooldownComponent;

        private ISpawner<Zombie> _spawner;

        [Inject]
        internal void Construct(ISpawner<Zombie> spawner)
        {
            _spawner = spawner;
        }

        public override void Compose()
        {
            base.Compose();

            _aliveCondition.Value = true;
            
            _zombieSpawnAction.Compose(() => _spawner.Spawn());

            _cooldownComponent.Let(it =>
            {
                it.Compose(_zombieSpawnAction);
                it.CoolDownCondition.Append(_aliveCondition);
            });
        }

        void IStartGameListener.OnStart()
        {
            Compose();
        }
        
        void IUpdateGameListener.OnUpdate()
        {
            _cooldownComponent.Update();
        }

        void IFinishGameListener.OnFinish()
        {
            _aliveCondition.Value = false;
        }
    }
}