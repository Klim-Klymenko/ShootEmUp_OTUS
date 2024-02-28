using System;
using GameEngine;
using UnityEngine;

namespace Objects
{
    [Serializable]
    internal sealed class Character_Animation
    {
        [SerializeField]
        private Animator _animator;

        [SerializeField] 
        private HealthAnimationComponent _healthAnimationComponent;

        private MoveAnimationController _moveAnimationController;
        
        private AttackAnimationController _attackAnimationController;
        
        internal void Compose(Character_Core core)
        {
            _healthAnimationComponent.Compose(core.TakeDamageObservable, core.DeathObservable, _animator);
            
            _moveAnimationController = new MoveAnimationController(core.MoveCondition, _animator);
            _attackAnimationController = new AttackAnimationController(core.ShootObservable, _animator);
        }
        
        internal void OnEnable()
        {
            _healthAnimationComponent.OnEnable();
            _attackAnimationController.OnEnable();
        }

        internal void Update()
        {
            _moveAnimationController.Update();
        }
        
        internal void OnDisable()
        {
            _healthAnimationComponent.OnDisable();
            _attackAnimationController.OnDisable();
        }
    }
}