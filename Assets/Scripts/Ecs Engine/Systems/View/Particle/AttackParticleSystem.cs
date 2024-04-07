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
        private readonly EcsFilterInject<Inc<AttackEvent, CurrentWeapon>, Exc<Inactive>> _filter;
        private readonly EcsPoolInject<AttackParticle> _attackParticlePoolInject;
        private readonly EcsWorldInject _world;
        
        private EcsPool<CurrentWeapon> _weaponPool;
        private EcsPool<AttackParticle> _attackParticlePool;
        
        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _weaponPool = _filter.Pools.Inc2;
            _attackParticlePool = _attackParticlePoolInject.Value;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int entityId in _filter.Value)
            {
                EcsPackedEntity weaponEntity = _weaponPool.Get(entityId).Value;

                if (!weaponEntity.Unpack(_world.Value, out int weaponEntityId))
                    throw new Exception("Weapon entity is unable to unpack");
                
                ParticleSystem attackParticle = _attackParticlePool.Get(weaponEntityId).Value;
                
                attackParticle.Play(withChildren: true);
            }
        }
    }
}