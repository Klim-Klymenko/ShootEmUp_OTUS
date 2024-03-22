using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using Common;
using GameCycle;
using GameEngine;
using JetBrains.Annotations;
using UnityEngine;

namespace System
{
    [UsedImplicitly]
    internal sealed class ShootInputController : IStartGameListener, IUpdateGameListener
    {
        private IAtomicAction _shootAction;
        private Countdown _fireCountdown;
        private Countdown _reloadCountdown;
        
        private readonly IAtomicObject _gun;
        
        internal ShootInputController(IAtomicObject gun)
        {
            _gun = gun;
        }

        void IStartGameListener.OnStart()
        {
            IAtomicValue<float> shootingInterval = _gun.GetValue<float>(ShooterAPI.ShootingInterval);
            
            _shootAction = _gun.GetAction(ShooterAPI.ShootAction);
            _fireCountdown = new Countdown(shootingInterval.Value);
            _reloadCountdown = new Countdown(shootingInterval.Value);
            
            _reloadCountdown.SetZero();
        }

        void IUpdateGameListener.OnUpdate()
        {
            _reloadCountdown.Tick(Time.deltaTime);
            
            if (Input.GetMouseButtonDown(0))
            {
                if (_reloadCountdown.IsPlaying()) return;
                
                Shoot();
                _reloadCountdown.Reset();
            }
            
            else if (Input.GetMouseButton(0))
            {
                _fireCountdown.Tick(Time.deltaTime);
            
                if (_fireCountdown.IsPlaying()) return;
                
                Shoot();
                _fireCountdown.Reset();
            }
            
            else if (Input.GetMouseButtonUp(0))
                _fireCountdown.Reset();
        }

        private void Shoot()
        {
            _shootAction.Invoke();
        }
    }
}