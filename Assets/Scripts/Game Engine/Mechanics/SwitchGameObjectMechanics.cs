using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    public sealed class SwitchGameObjectMechanics
    {
        private readonly GameObject _switchable;
        private readonly IAtomicObservable _switchOnObservable;
        private readonly IAtomicObservable _switchOffObservable;

        public SwitchGameObjectMechanics(GameObject switchable, IAtomicObservable switchOnObservable, IAtomicObservable switchOffObservable)
        {
            _switchable = switchable;
            _switchOnObservable = switchOnObservable;
            _switchOffObservable = switchOffObservable;
        }
        
        public void OnEnable()
        {
            _switchOnObservable.Subscribe(SwitchOn);
            _switchOffObservable.Subscribe(SwitchOff);
        }
        
        public void OnDisable()
        {
            _switchOnObservable.Unsubscribe(SwitchOn);
            _switchOffObservable.Unsubscribe(SwitchOff);
        }
        
        private void SwitchOn()
        {
            if (_switchable != null) 
                _switchable.SetActive(true);
        }
        
        private void SwitchOff()
        {
            if (_switchable != null) 
                _switchable.SetActive(false);
        }
    }
}