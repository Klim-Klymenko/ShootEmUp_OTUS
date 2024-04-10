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
        private readonly EcsFilterInject<Inc<AttackEvent, CurrentWeapon>, Exc<Inactive>> _filterInject;
        private readonly EcsPoolInject<UnityAudioSource> _audioSourcePoolInject;
        private readonly EcsPoolInject<AttackClip> _attackClipPoolInject;
        private readonly EcsWorldInject _worldInject;
        
        private EcsPool<CurrentWeapon> _weaponPool;
        
        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _weaponPool = _filterInject.Pools.Inc2;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int entityId in _filterInject.Value)
            {
                EcsPackedEntity weaponEntity = _weaponPool.Get(entityId).Value;
                
                if (!weaponEntity.Unpack(_worldInject.Value, out int weaponEntityId))
                    throw new Exception("Weapon entity is unable to unpack");
                
                AudioSource audioSource = _audioSourcePoolInject.Value.Get(weaponEntityId).Value;
                AudioClip attackClip = _attackClipPoolInject.Value.Get(weaponEntityId).Value;
                
                audioSource.PlayOneShot(attackClip);
            }
        }
    }
}