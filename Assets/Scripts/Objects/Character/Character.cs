using Atomic.Objects;
using GameCycle;
using GameEngine;
using UnityEngine;
using Zenject;

namespace Objects
{
    [Is(ObjectTypes.Character)]
    public sealed class Character : AtomicObject, IInitializeGameListener, IUpdateGameListener, IFinishGameListener
    {
        [Section]
        [SerializeField]
        private Character_Core _core;
        
        [SerializeField]
        private Character_Animation _animation;
        
        [SerializeField]
        private Character_FX _fx;

        private DiContainer _diContainer;
        
        [Inject]
        public void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }
        
        public override void Compose()
        {
            base.Compose();
            
            _core.Compose(_diContainer);
            _animation.Compose(_core);
            _fx.Compose(_core);
            
            _core.OnEnable();
            _animation.OnEnable();
            _fx.OnEnable();
        }

        void IInitializeGameListener.OnInitialize()
        {
            Compose();
        }

        void IUpdateGameListener.OnUpdate()
        {
            _core.Update();
            _animation.Update();
            _fx.Update();
        }

        void IFinishGameListener.OnFinish()
        {
            _core.OnDisable();
            _animation.OnDisable();
            _fx.OnDisable();
            
            _core.Dispose();
            _fx.Dispose();
        }
    }
}