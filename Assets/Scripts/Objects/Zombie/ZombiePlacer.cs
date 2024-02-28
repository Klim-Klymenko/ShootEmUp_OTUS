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
    internal sealed class ZombiePlacer : AtomicObject, IStartGameListener, IUpdateGameListener
    {
        [SerializeField]
        private AtomicObject _character;
        
        [SerializeField]
        private AtomicValue<float> _spawnInterval;
        
        [SerializeField]
        private AtomicAction _zombieSpawnAction;

        private ISpawner<Zombie> _spawner;
        
        private CooldownMechanics _cooldownMechanics;
        
        [Inject]
        internal void Construct(ISpawner<Zombie> spawner)
        {
            _spawner = spawner;
        }

        public override void Compose()
        {
            base.Compose();
            
            IAtomicValue<bool> isAlive = _character.GetValue<bool>(LiveableAPI.AliveCondition);
            
            _zombieSpawnAction.Compose(() => _spawner.Spawn());
            
            _cooldownMechanics = new CooldownMechanics(_zombieSpawnAction, _spawnInterval, isAlive);
        }

        void IStartGameListener.OnStart()
        {
            Compose();
        }
        
        void IUpdateGameListener.OnUpdate()
        {
            _cooldownMechanics.Update();
        }
    }
}