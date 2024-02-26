using Atomic.Elements;
using Common;
using UnityEngine;

namespace GameEngine
{
    public sealed class ShootInputController
    {
        private readonly IAtomicAction _shootAction;
        private readonly Countdown _fireCountdown;
        private readonly Countdown _reloadCountdown;

        public ShootInputController(IAtomicAction shootAction, IAtomicValue<float> shootInterval)
        {
            _shootAction = shootAction;
            _fireCountdown = new Countdown(shootInterval.Value);
            _reloadCountdown = new Countdown(shootInterval.Value);
            
            _reloadCountdown.SetZero();
        }

        public void Update()
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