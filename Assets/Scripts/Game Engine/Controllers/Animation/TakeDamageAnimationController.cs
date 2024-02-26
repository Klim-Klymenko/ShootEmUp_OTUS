using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    public sealed class TakeDamageAnimationController
    {
        private static readonly int _takeDamageTrigger = Animator.StringToHash("Take Damage");
        
        private readonly IAtomicObservable<int> _takeDamageEvent;
        private readonly Animator _animator;

        public TakeDamageAnimationController(IAtomicObservable<int> takeDamageEvent, Animator animator)
        {
            _takeDamageEvent = takeDamageEvent;
            _animator = animator;
        }

        public void OnEnable()
        {
            _takeDamageEvent.Subscribe(OnTakeDamage);
        }
        
        public void OnDisable()
        {
            _takeDamageEvent.Unsubscribe(OnTakeDamage);
        }
        
        private void OnTakeDamage(int _)
        {
            _animator.SetTrigger(_takeDamageTrigger);
        }
    }
}