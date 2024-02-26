using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    public sealed class AttackAnimationController
    {
        private static readonly int _attackTrigger = Animator.StringToHash("Attack");
        
        private readonly IAtomicObservable _attackEvent;
        private readonly Animator _animator;

        public AttackAnimationController(IAtomicObservable attackEvent, Animator animator)
        {
            _attackEvent = attackEvent;
            _animator = animator;
        }
        
        public void OnEnable()
        {
            _attackEvent.Subscribe(OnShoot);
        }
        
        public void OnDisable()
        {
            _attackEvent.Unsubscribe(OnShoot);
        }

        private void OnShoot()
        {
            _animator.SetTrigger(_attackTrigger);
        }
    }
}