using System;
using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class HealthAnimationComponent
    {
        private TakeDamageAnimationController _takeDamageAnimationController;
        private DeathAnimationController _deathAnimationController;
        
        public void Compose(IAtomicObservable<int> takeDamageEvent, IAtomicObservable deathEvent, Animator animator)
        {
            _takeDamageAnimationController = new TakeDamageAnimationController(takeDamageEvent, animator);
            _deathAnimationController = new DeathAnimationController(deathEvent, animator);
        }
        
        public void OnEnable()
        {
            _takeDamageAnimationController.OnEnable();
            _deathAnimationController.OnEnable();
        }

        public void OnDisable()
        {
            _takeDamageAnimationController.OnDisable();
            _deathAnimationController.OnDisable();
        }
    }
}