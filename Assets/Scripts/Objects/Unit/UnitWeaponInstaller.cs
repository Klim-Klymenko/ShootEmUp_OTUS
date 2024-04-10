using EcsEngine.Extensions;
using Leopotam.EcsLite;
using UnityEngine;

namespace Objects.Swordsman
{
    internal sealed class UnitWeaponInstaller : EntityInstaller
    {
        [SerializeField] 
        private Entity _weaponEntity;
        
        public override void Install(Entity entity, EcsWorld world)
        {
            if (!_weaponEntity.Unpack(out int _))
                _weaponEntity.Initialize(world);
        }

        public override void Uninstall(Entity entity, EcsWorld world)
        {
            if (_weaponEntity.Unpack(out int _))
                _weaponEntity.Dispose();
        }
    }
}