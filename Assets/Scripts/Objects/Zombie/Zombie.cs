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
        private Zombie_AI _ai;
        
        [Section]
        [SerializeField]
        private Zombie_Animation _animation;
        
        [SerializeField]
        private Zombie_Audio _audio;

        private bool _composed;
        
        public override void Compose()
        {
            base.Compose();

            _core.Compose();
            _ai.Compose(_core);
            _animation.Compose(_core, _ai);
            _audio.Compose(_core);
            
            _core.OnEnable();
            _ai.OnEnable();
            _animation.OnEnable();
            _audio.OnEnable();

            _composed = true;
        }
        
        void IUpdateGameListener.OnUpdate()
        {
            if (!_composed) return;
            
            _core.Update();
            _ai.Update();
            _animation.Update();
        }

        public void OnFinish()
        {
            if (!_composed) return;
            
            _core.OnDisable();
            _ai.OnDisable();
            _animation.OnDisable();
            _audio.OnDisable();
            
            _core?.Dispose();
            _ai?.Dispose();
        }
    }
}