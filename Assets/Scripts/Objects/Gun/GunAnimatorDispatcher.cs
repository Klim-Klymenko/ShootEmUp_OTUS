using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;
using GameCycle;
using GameEngine;

namespace Objects
{
    public sealed class GunAnimatorDispatcher : MonoBehaviour, IStartGameListener
    {
        [SerializeField]
        private AtomicObject _gun;
        
        private IAtomicAction _switchOnEvent;
        private IAtomicAction _switchOffEvent;
        
        void IStartGameListener.OnStart()
        {
            _switchOnEvent = _gun.Get<IAtomicAction>(GunAPI.SwitchOnEvent);
            _switchOffEvent = _gun.Get<IAtomicAction>(GunAPI.SwitchOffEvent);
        }

        public void SwitchOnGun()
        {
            _switchOnEvent?.Invoke();
        }
        
        public void SwitchOffGun()
        {
            _switchOffEvent?.Invoke();
        }
    }
}