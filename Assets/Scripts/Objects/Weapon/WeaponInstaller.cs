using EcsEngine.Components;
using EcsEngine.Components.View;
using EcsEngine.Extensions;
using Leopotam.EcsLite;
using UnityEngine;

namespace Objects.Swordsman
{
    public sealed class WeaponInstaller : EntityInstaller
    {
        [SerializeField] 
        private Timer _timer;
        
        [SerializeField]
        private Damage _damage;

        [SerializeField]
        private WeaponType _weaponType;

        [SerializeField] 
        private Spawn _spawn;
        
        [SerializeField]
        private UnityAudioSource _audioSource;
        
        [SerializeField]
        private AttackClip _attackClip;
        
        [SerializeField]
        private AttackParticle _attackParticle;
        
        public override void Install(Entity entity, EcsWorld world)
        {
            entity
                .AddComponent(_timer)
                .AddComponent(_damage)
                .AddComponent(_weaponType)
                .AddComponent(_spawn)
                .AddComponent(_audioSource)
                .AddComponent(_attackClip)
                .AddComponent(_attackParticle);
        }
    }
}