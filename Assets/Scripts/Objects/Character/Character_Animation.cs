using System;
using GameEngine;
using UnityEngine;

namespace Objects
{
    [Serializable]
    public sealed class Character_Animation
    {
        [SerializeField]
        private Animator _animator;

        [SerializeField] 
        private HealthAnimationComponent _healthAnimationComponent;

        private MoveAnimationController _moveAnimationController;
        
        private AttackAnimationController _attackAnimationController;
        
        public void Compose(Character_Core core)
        {
            _healthAnimationComponent.Compose(core.TakeDamageObservable, core.DeathObservable, _animator);
            
            _moveAnimationController = new MoveAnimationController(core.MoveCondition, _animator);
            _attackAnimationController = new AttackAnimationController(core.ShootObservable, _animator);
        }
        
        public void OnEnable()
        {
            _healthAnimationComponent.OnEnable();
            _attackAnimationController.OnEnable();
        }

        public void Update()
        {
            _moveAnimationController.Update();
        }
        
        public void OnDisable()
        {
            _healthAnimationComponent.OnDisable();
            _attackAnimationController.OnDisable();
        }
    }
}