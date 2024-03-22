using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using UnityEngine;
using GameCycle;
using GameEngine;

namespace Objects
{
    internal sealed class GunAnimatorDispatcher : MonoBehaviour, IStartGameListener
    {
        [SerializeField]
        private AtomicObject _gun;
        
        private IAtomicAction _switchOnEvent;
        private IAtomicAction _switchOffEvent;
        
        void IStartGameListener.OnStart()
        {
            _switchOnEvent = _gun.GetAction(SwitchableAPI.SwitchOnAction);
            _switchOffEvent = _gun.GetAction(SwitchableAPI.SwitchOffAction);
        }

        internal void SwitchOnGun()
        {
            _switchOnEvent?.Invoke();
        }
        
        internal void SwitchOffGun()
        {
            _switchOffEvent?.Invoke();
        }
    }
}