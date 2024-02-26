using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    public sealed class SwitchGameObjectMechanics
    {
        private readonly GameObject _switchable;
        private readonly IAtomicObservable _switchOnEvent;
        private readonly IAtomicObservable _switchOffEvent;

        public SwitchGameObjectMechanics(GameObject switchable, IAtomicObservable switchOnEvent, IAtomicObservable switchOffEvent)
        {
            _switchable = switchable;
            _switchOnEvent = switchOnEvent;
            _switchOffEvent = switchOffEvent;
        }
        
        public void OnEnable()
        {
            _switchOnEvent.Subscribe(SwitchOn);
            _switchOffEvent.Subscribe(SwitchOff);
        }
        
        public void OnDisable()
        {
            _switchOnEvent.Unsubscribe(SwitchOn);
            _switchOffEvent.Unsubscribe(SwitchOff);
        }
        
        private void SwitchOn()
        {
            _switchable.SetActive(true);
        }
        
        private void SwitchOff()
        {
            _switchable.SetActive(false);
        }
    }
}