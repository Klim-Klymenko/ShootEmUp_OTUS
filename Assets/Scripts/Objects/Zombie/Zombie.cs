using Atomic.Objects;
using GameCycle;
using GameEngine;
using UnityEngine;

namespace Objects
{
    [Is(ObjectTypes.Zombie)]
    internal sealed class Zombie : AtomicObject, IUpdateGameListener, IFinishGameListener
    {
        [SerializeField]
        [Get(ZombieAPI.ZombieAnimatorDispatcher)]
        private ZombieAnimatorDispatcher _zombieAnimatorDispatcher;
        
        [Section]
        [SerializeField]
        private Zombie_Core _core;
        
        [Section]
        [SerializeField]
        private Zombie_Animation _animation;
        
        [SerializeField]
        private Zombie_FX _fx;

        private bool _composed;
        
        public override void Compose()
        {
            base.Compose();

            _core.Compose();
            _animation.Compose(_core);
            _fx.Compose(_core);
            
            _core.OnEnable();
            _animation.OnEnable();
            _fx.OnEnable();

            _composed = true;
        }
        
        void IUpdateGameListener.OnUpdate()
        {
            if (!_composed) return;
            
            _core.Update();
            _animation.Update();
        }

        public void OnFinish()
        {
            if (!_composed) return;
            
            _core.OnDisable();
            _animation.OnDisable();
            _fx.OnDisable();
            
            _core?.Dispose();
        }
    }
}