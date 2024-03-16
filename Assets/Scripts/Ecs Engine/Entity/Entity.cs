using Leopotam.EcsLite;
using UnityEngine;

namespace EcsEngine
{
    public sealed class Entity : MonoBehaviour
    {
        [SerializeField]
        private EntityInstaller[] _entityInstallers;

        private EcsWorld _world;
        private EcsPackedEntity _packedEntity;
        
        public EcsPackedEntity PackedEntity => _packedEntity;
        
        public void Initialize(EcsWorld world)
        {
            _world = world;
            
            int entityId = world.NewEntity();
            world.PackEntity(entityId);

            for (int i = 0; i < _entityInstallers.Length; i++)
                _entityInstallers[i].Install(this);
        }
        
        public void Destroy()
        {
            for (int i = 0; i < _entityInstallers.Length; i++)
                _entityInstallers[i].Uninstall(this);
            
            if (_packedEntity.Unpack(_world, out int entityId))
                _world.DelEntity(entityId);

            _world = null;
            _packedEntity = default;
        }
    }
}