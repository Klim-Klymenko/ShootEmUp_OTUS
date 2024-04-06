using System.IO;
using Leopotam.EcsLite;
using UnityEngine;

namespace EcsEngine.Extensions
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
            _packedEntity = world.PackEntity(entityId);
            
            for (int i = 0; i < _entityInstallers.Length; i++)
                _entityInstallers[i].Install(this);
        }
        
        public void Dispose()
        {
            for (int i = 0; i < _entityInstallers.Length; i++)
                _entityInstallers[i].Uninstall(this);
            
            if (Unpack(out int entityId))
                _world.DelEntity(entityId);

            _world = null;
            _packedEntity = default;
        }
        
        public bool IsAlive()
        {
            return Unpack(out int _);
        }

        public bool Unpack(out int entityId)
        {
            return _packedEntity.Unpack(_world, out entityId);
        }
        
        public Entity AddComponent<T>(T component) where T : struct
        {
            if (!Unpack(out int entityId))
                throw new InvalidDataException("Failed to unpack entity");
            
            EcsPool<T> pool = _world.GetPool<T>();
            pool.Add(entityId) = component;

            return this;
        }

        public Entity SetComponent<T>(T component) where T : struct
        {
            if (!Unpack(out int entityId))
                throw new InvalidDataException("Failed to unpack entity");
            
            EcsPool<T> pool = _world.GetPool<T>();
            
            if (pool.Has(entityId))
                pool.Get(entityId) = component;
            else
                pool.Add(entityId) = component;

            return this;
        }

        public new ref T GetComponent<T>() where T : struct
        {
            if (!Unpack(out int entityId))
                throw new InvalidDataException("Failed to unpack entity");
            
            EcsPool<T> pool = _world.GetPool<T>();
            return ref pool.Get(entityId);
        }
        
        public Entity RemoveComponent<T>() where T : struct
        {
            if (!Unpack(out int entityId))
                throw new InvalidDataException("Failed to unpack entity");
            
            EcsPool<T> pool = _world.GetPool<T>();
            pool.Del(entityId);

            return this;
        }
    }
}