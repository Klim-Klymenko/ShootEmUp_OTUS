using EcsEngine.Components;
using EcsEngine.Components.View;
using EcsEngine.Components.View;
using EcsEngine.Extensions;
using UnityEngine;
using UnityEngine.Serialization;

namespace Objects.Swordsman
{
    public sealed class SwordsmanViewInstaller : EntityInstaller
    {
        [SerializeField]
        private UnityTransform _transform;

        [SerializeField]
        private UnityAnimator _animator;
        
        [SerializeField]
        private MoveAnimation _moveAnimation;
        
        [SerializeField]
        private AttackAnimation _attackAnimation;
        
        [SerializeField]
        private TakeDamageAnimation _takeDamageAnimation;

        [SerializeField]
        private UnityAudioSource _audioSource;
        
        public override void Install(Entity entity)
        {
            entity
                .AddComponent(_transform)
                .AddComponent(_animator)
                .AddComponent(_moveAnimation)
                .AddComponent(_attackAnimation)
                .AddComponent(_takeDamageAnimation)
                .AddComponent(_audioSource);
        }
    }
}