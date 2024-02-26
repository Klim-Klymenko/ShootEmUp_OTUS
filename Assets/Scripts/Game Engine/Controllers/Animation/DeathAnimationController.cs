using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    public sealed class DeathAnimationController
    {
        private static readonly int _deathTrigger = Animator.StringToHash("Death");
        
        private readonly IAtomicObservable _deathEvent;
        private readonly Animator _animator;

        public DeathAnimationController(IAtomicObservable deathEvent, Animator animator)
        {
            _deathEvent = deathEvent;
            _animator = animator;
        }
        
        public void OnEnable()
        {
            _deathEvent.Subscribe(OnDeath);
        }
        
        public void OnDisable()
        {
            _deathEvent.Unsubscribe(OnDeath);
        }

        private void OnDeath()
        {
            _animator.SetTrigger(_deathTrigger);
        }
    }
}