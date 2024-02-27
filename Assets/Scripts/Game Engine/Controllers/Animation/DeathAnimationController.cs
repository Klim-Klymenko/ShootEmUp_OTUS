using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    public sealed class DeathAnimationController
    {
        private static readonly int _deathTrigger = Animator.StringToHash("Death");
        
        private readonly IAtomicObservable _deathObservable;
        private readonly Animator _animator;

        public DeathAnimationController(IAtomicObservable deathObservable, Animator animator)
        {
            _deathObservable = deathObservable;
            _animator = animator;
        }
        
        public void OnEnable()
        {
            _deathObservable.Subscribe(OnDeath);
        }
        
        public void OnDisable()
        {
            _deathObservable.Unsubscribe(OnDeath);
        }

        private void OnDeath()
        {
            _animator.SetTrigger(_deathTrigger);
        }
    }
}