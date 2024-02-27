using System;
using Atomic.Elements;
using Atomic.Objects;
using GameCycle;
using GameEngine;

namespace Objects
{
    public sealed class Gun : AtomicObject, IInitializeGameListener, IFinishGameListener, IDisposable
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
            _switchGameObjectMechanics.OnDisable();
            Dispose();
        }

        public void Dispose()
        {
            _switchOnEvent?.Dispose();
            _switchOffEvent?.Dispose();
        }
    }
}