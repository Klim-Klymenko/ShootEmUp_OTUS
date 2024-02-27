using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    public sealed class AttackAnimationController
    {
        private static readonly int _attackTrigger = Animator.StringToHash("Attack");
        
        private readonly IAtomicObservable _attackObservable;
        private readonly Animator _animator;

        public AttackAnimationController(IAtomicObservable attackObservable, Animator animator)
        {
            _attackObservable = attackObservable;
            _animator = animator;
        }
        
        public void OnEnable()
        {
            _attackObservable.Subscribe(OnShoot);
        }
        
        public void OnDisable()
        {
            _attackObservable.Unsubscribe(OnShoot);
        }

        private void OnShoot()
        {
            _animator.SetTrigger(_attackTrigger);
        }
    }
}