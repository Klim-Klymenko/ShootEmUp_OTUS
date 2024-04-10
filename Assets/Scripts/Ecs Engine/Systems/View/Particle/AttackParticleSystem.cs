using System;
using EcsEngine.Components;
using EcsEngine.Components.Events;
using EcsEngine.Components.Tags;
using EcsEngine.Components.View;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems.View.Particle
{
    public sealed class AttackParticleSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AttackEvent, CurrentWeapon>, Exc<Inactive>> _filterInject;
        private readonly EcsPoolInject<AttackParticle> _attackParticlePoolInject;
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
                
                ParticleSystem attackParticle = _attackParticlePoolInject.Value.Get(weaponEntityId).Value;
                
                attackParticle.Play(withChildren: true);
            }
        }
    }
}