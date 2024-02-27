using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    public sealed class TakeDamageAnimationController
    {
        private static readonly int _takeDamageTrigger = Animator.StringToHash("Take Damage");
        
        private readonly IAtomicObservable<int> _takeDamageObservable;
        private readonly Animator _animator;

        public TakeDamageAnimationController(IAtomicObservable<int> takeDamageObservable, Animator animator)
        {
            _takeDamageObservable = takeDamageObservable;
            _animator = animator;
        }

        public void OnEnable()
        {
            _takeDamageObservable.Subscribe(OnTakeDamage);
        }
        
        public void OnDisable()
        {
            _takeDamageObservable.Unsubscribe(OnTakeDamage);
        }
        
        private void OnTakeDamage(int _)
        {
            _animator.SetTrigger(_takeDamageTrigger);
        }
    }
}