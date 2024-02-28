using Atomic.Elements;
using Atomic.Objects;
using GameCycle;
using GameEngine;

namespace Objects
{
    internal sealed class Gun : AtomicObject, IInitializeGameListener, IFinishGameListener
    {
        [Get(SwitchableAPI.SwitchOnAction)]
        private AtomicEvent _switchOnEvent = new();
        
        [Get(SwitchableAPI.SwitchOffAction)]
        private AtomicEvent _switchOffEvent = new();
        
        private SwitchGameObjectMechanics _switchGameObjectMechanics;

        public override void Compose()
        {
            base.Compose();
            
            _switchGameObjectMechanics = new SwitchGameObjectMechanics(gameObject, _switchOnEvent, _switchOffEvent);
            _switchGameObjectMechanics.OnEnable();
        }

        void IInitializeGameListener.OnInitialize()
        {
            Compose();
        }

        void IFinishGameListener.OnFinish()
        {
            _switchOffEvent?.Invoke();
            _switchGameObjectMechanics.OnDisable();
            
            _switchOnEvent?.Dispose();
            _switchOffEvent?.Dispose();
        }
    }
}