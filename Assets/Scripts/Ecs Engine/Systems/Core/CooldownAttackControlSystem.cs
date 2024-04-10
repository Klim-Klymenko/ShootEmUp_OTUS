using System;
using EcsEngine.Components;
using EcsEngine.Components.Requests;
using EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    public sealed class CooldownAttackControlSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AttackEnabled, CurrentWeapon>, Exc<AttackRequest>> _filterInject;
        private readonly EcsPoolInject<Tickable> _tickablePoolInject;
        private readonly EcsPoolInject<AttackRequest> _attackRequestPoolInject;
        private readonly EcsWorldInject _gameObjectsWorld;

        private EcsPool<CurrentWeapon> _weaponPool;
        private EcsPool<Tickable> _tickablePool;
        private EcsPool<AttackRequest> _attackRequestPool;

        void IEcsPreInitSystem.PreInit(IEcsSystems systems)
        {
            _weaponPool = _filterInject.Pools.Inc2;
            _tickablePool = _tickablePoolInject.Value;
            _attackRequestPool = _attackRequestPoolInject.Value;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int entityId in _filterInject.Value)
            {
                EcsPackedEntity weaponEntity = _weaponPool.Get(entityId).Value;
                
                if (!weaponEntity.Unpack(_gameObjectsWorld.Value, out int weaponEntityId)) 
                    throw new Exception("Weapon entity is unable to unpack"); 
            
                if (_tickablePool.Has(weaponEntityId)) continue;
                
                _tickablePool.Add(weaponEntityId) = new Tickable();
                _attackRequestPool.Add(entityId) = new AttackRequest();
            }
        }
    }
}