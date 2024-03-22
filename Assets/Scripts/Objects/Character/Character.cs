using Atomic.Objects;
using GameCycle;
using GameEngine;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Objects
{
    [Is(ObjectTypes.Character)]
    internal sealed class Character : AtomicObject, IInitializeGameListener, IUpdateGameListener, IFinishGameListener
    {
        [Section]
        [SerializeField]
        private Character_Core _core;
        
        [SerializeField]
        private Character_Animation _animation;

        [SerializeField]
        private Character_Audio _audio;

        [SerializeField] 
        private Character_Particle _particle;
        
        public override void Compose()
        {
            base.Compose();
            
            _core.Compose();
            _animation.Compose(_core);
            _audio.Compose(_core);
            _particle.Compose(_core);
            
            _core.OnEnable();
            _animation.OnEnable();
            _audio.OnEnable();
        }

        void IInitializeGameListener.OnInitialize()
        {
            Compose();
        }

        void IUpdateGameListener.OnUpdate()
        {
            _core.Update();
            _animation.Update();
            _audio.Update();
            _particle.Update();
        }

        void IFinishGameListener.OnFinish()
        {
            _core.OnDisable();
            _animation.OnDisable();
            _audio.OnDisable();
            
            _core.Dispose();
            _audio.Dispose();
            _particle.Dispose();
        }
    }
}