using UnityEngine;

namespace GameEngine
{
    public sealed class SwitchAnimatorMechanics
    {
        private readonly Animator _animator;

        public SwitchAnimatorMechanics(Animator animator)
        {
            _animator = animator;
        }

        public void OnEnable()
        {
            if (_animator.enabled) return;
            
            _animator.enabled = true;
        }

        public void OnDisable()
        {
            if (_animator == null || !_animator.enabled) return;
            
            _animator.enabled = false;
        }
    }
}