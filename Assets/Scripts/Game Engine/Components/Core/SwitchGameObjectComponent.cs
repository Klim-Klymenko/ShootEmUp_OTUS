using System;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class SwitchGameObjectComponent : IDisposable
    {
        [Get(SwitchableAPI.SwitchOnAction)]
        private readonly AtomicEvent _switchOnEvent = new();
        
        [Get(SwitchableAPI.SwitchOffAction)]
        private readonly AtomicEvent _switchOffEvent = new();
        
        private SwitchGameObjectMechanics _switchGameObjectMechanics;

        public void Compose(GameObject gameObject)
        {
            _switchGameObjectMechanics = new SwitchGameObjectMechanics(gameObject, _switchOnEvent, _switchOffEvent);
        }

        public void OnEnable()
        {
            _switchGameObjectMechanics.OnEnable();
        }
        
        public void OnDisable()
        {
            _switchOffEvent?.Invoke();
            _switchGameObjectMechanics.OnDisable();
        }
        
        public void Dispose()
        {
            _switchOnEvent?.Dispose();
            _switchOffEvent?.Dispose();
        }
    }
}