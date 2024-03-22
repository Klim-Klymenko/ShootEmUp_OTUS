using System;
using Atomic.Elements;
using GameEngine;
using UnityEngine;

namespace Objects
{
    [Serializable]
    internal sealed class Character_Animation
    {
        [SerializeField]
        private Animator _animator;
        
        private TakeDamageAnimationController _takeDamageAnimationController;
        private DeathAnimationController _deathAnimationController;
        private MoveAnimationController _moveAnimationController;
        private AttackAnimationController _attackAnimationController;
        
        internal void Compose(Character_Core core)
        {
            IAtomicObservable<int> takeDamageObservable = core.TakeDamageObservable;
            IAtomicObservable deathObservable = core.DeathObservable;
            
            _takeDamageAnimationController = new TakeDamageAnimationController(takeDamageObservable, _animator);
            _deathAnimationController = new DeathAnimationController(deathObservable, _animator);
            _moveAnimationController = new MoveAnimationController(core.MoveCondition, _animator);
            _attackAnimationController = new AttackAnimationController(core.ShootObservable, _animator);
        }
        
        internal void OnEnable()
        {
            _takeDamageAnimationController.OnEnable();
            _deathAnimationController.OnEnable();
            _attackAnimationController.OnEnable();
        }

        internal void Update()
        {
            _moveAnimationController.Update();
        }
        
        internal void OnDisable()
        {
            _takeDamageAnimationController.OnDisable();
            _deathAnimationController.OnDisable();
            _attackAnimationController.OnDisable();
        }
    }
}