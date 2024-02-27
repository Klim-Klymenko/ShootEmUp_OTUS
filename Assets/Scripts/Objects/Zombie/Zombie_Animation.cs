using System;
using Atomic.Elements;
using Atomic.Objects;
using GameEngine;
using UnityEngine;

namespace Objects
{
    [Serializable]
    public sealed class Zombie_Animation
    {
        [SerializeField]
        [Get(VisualAPI.SkinnedMeshRenderer)]
        private SkinnedMeshRenderer _skinnedMeshRenderer;
        
        [SerializeField]
        private Animator _animator;
        
        private SwitchSkinnedMeshRendererMechanics _switchSkinnedMeshRendererMechanics;
        private MoveAnimationController _moveAnimationController;
        private AttackAnimationController _attackAnimationController;
        private SwitchAnimatorMechanics _switchAnimatorMechanics;
        
        public void Compose(Zombie_Core core)
        {
            IAtomicValue<bool> moveCondition = core.MoveCondition;
            IAtomicObservable attackRequestEvent = core.AttackRequestEvent;

            _switchSkinnedMeshRendererMechanics = new SwitchSkinnedMeshRendererMechanics(_skinnedMeshRenderer);
            _switchAnimatorMechanics = new SwitchAnimatorMechanics(_animator);
            _moveAnimationController = new MoveAnimationController(moveCondition, _animator);
            _attackAnimationController = new AttackAnimationController(attackRequestEvent, _animator);
        }

        public void OnEnable()
        {
            _switchSkinnedMeshRendererMechanics.OnEnable();
            _switchAnimatorMechanics.OnEnable();
            _attackAnimationController.OnEnable();
        }

        public void Update()
        {
            _moveAnimationController.Update();
        }

        public void OnDisable()
        {
            _attackAnimationController.OnDisable();
            _switchAnimatorMechanics.OnDisable();
        }
    }
}