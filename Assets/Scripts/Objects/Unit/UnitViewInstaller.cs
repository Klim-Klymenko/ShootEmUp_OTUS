using EcsEngine.Components;
using EcsEngine.Components.View;
using EcsEngine.Extensions;
using Leopotam.EcsLite;
using UnityEngine;

namespace Objects.Swordsman
{
    internal sealed class UnitViewInstaller : EntityInstaller
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
        
        [SerializeField]
        private TakeDamageClip _takeDamageClip;
        
        [SerializeField]
        private TakeDamageParticle _takeDamageParticle;
        
        public override void Install(Entity entity, EcsWorld world)
        {
            entity
                .AddComponent(_transform)
                .AddComponent(_animator)
                .AddComponent(_moveAnimation)
                .AddComponent(_attackAnimation)
                .AddComponent(_takeDamageAnimation)
                .AddComponent(_audioSource)
                .AddComponent(_takeDamageClip)
                .AddComponent(_takeDamageParticle);
        }
    }
}