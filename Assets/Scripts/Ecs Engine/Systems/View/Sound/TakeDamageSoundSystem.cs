using EcsEngine.Components;
using EcsEngine.Components.Events;
using EcsEngine.Components.Tags;
using EcsEngine.Components.View;
using EcsEngine.Extensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems.Sound
{
    public sealed class TakeDamageSoundSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DealDamageEvent, Target>> _filterInject = EcsWorldsAPI.EventsWorld;
        private readonly EcsPoolInject<UnityAudioSource> _audioSourcePoolInject;
        private readonly EcsPoolInject<TakeDamageClip> _damageClipPoolInject;
        private readonly EcsPoolInject<Inactive> _inactivePoolInject;
        private readonly EcsWorldInject _worldInject;

        private EcsPool<Target> _targetPool;
        
        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _targetPool = _filterInject.Pools.Inc2;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int eventId in _filterInject.Value)
            {
                EcsPackedEntity targetEntity = _targetPool.Get(eventId).Value;
                
                if (!targetEntity.Unpack(_worldInject.Value, out int targetEntityId)) continue;
                
                if (_inactivePoolInject.Value.Has(targetEntityId)) continue;

                AudioSource audioSource = _audioSourcePoolInject.Value.Get(targetEntityId).Value;
                AudioClip damageClip = _damageClipPoolInject.Value.Get(targetEntityId).Value;
                
                audioSource.PlayOneShot(damageClip);
            }
        }
    }
}