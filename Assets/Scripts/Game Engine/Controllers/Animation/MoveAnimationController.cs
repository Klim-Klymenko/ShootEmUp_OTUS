using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    public sealed class MoveAnimationController
    {
        private static readonly int _moveInteger = Animator.StringToHash("Speed");
        private const int IdleSpeed = 0;
        private const int RunSpeed = 1;

        private readonly IAtomicValue<bool> _moveCondition;
        private readonly Animator _animator;

        public MoveAnimationController(IAtomicValue<bool> moveCondition, Animator animator)
        {
            _moveCondition = moveCondition;
            _animator = animator;
        }

        public void Update()
        {
            OnMove();
        }
        
        private void OnMove()
        {
            if (_moveCondition.Value)
                _animator.SetInteger(_moveInteger, RunSpeed);
            else
                _animator.SetInteger(_moveInteger, IdleSpeed);
        }
    }
}