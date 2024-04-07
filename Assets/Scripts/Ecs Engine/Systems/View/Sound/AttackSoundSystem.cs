using System;
using EcsEngine.Components;
using EcsEngine.Components.Events;
using EcsEngine.Components.Tags;
using EcsEngine.Components.View;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems.Sound
{
    public sealed class AttackSoundSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AttackEvent, CurrentWeapon>, Exc<Inactive>> _filter;
        private readonly EcsPoolInject<UnityAudioSource> _audioSourcePoolInject;
        private readonly EcsPoolInject<AttackClip> _attackClipPoolInject;
        private readonly EcsWorldInject _world;
        
        private EcsPool<CurrentWeapon> _weaponPool;
        private EcsPool<UnityAudioSource> _audioSourcePool;
        private EcsPool<AttackClip> _attackClipPool;
        
        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _weaponPool = _filter.Pools.Inc2;
            _audioSourcePool = _audioSourcePoolInject.Value;
            _attackClipPool = _attackClipPoolInject.Value;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int entityId in _filter.Value)
            {
                EcsPackedEntity weaponEntity = _weaponPool.Get(entityId).Value;
                
                if (!weaponEntity.Unpack(_world.Value, out int weaponEntityId))
                    throw new Exception("Weapon entity is unable to unpack");
                
                AudioSource audioSource = _audioSourcePool.Get(weaponEntityId).Value;
                AudioClip attackClip = _attackClipPool.Get(weaponEntityId).Value;
                
                audioSource.PlayOneShot(attackClip);
            }
        }
    }
}