using System;
using System.IO;
using Leopotam.EcsLite;
using UnityEngine;

namespace EcsEngine.Extensions
{
    public sealed class Entity : MonoBehaviour
    {
        [SerializeField]
        private string _name;

        [field: SerializeField] 
        public bool InstallOnAwake { get; private set; } = true;
            
        [SerializeField]
        private EntityInstaller[] _entityInstallers;
        
        private EcsWorld _world;
        private EcsPackedEntity _packedEntity;
        
        public EcsPackedEntity PackedEntity => _packedEntity;
        public string Name => _name;
        
        private void Reset()
        {
            _name = name;
        }

        public void Initialize(EcsWorld world)
        {
            _world = world;
            
            int entityId = world.NewEntity();
            _packedEntity = world.PackEntity(entityId);
            
            for (int i = 0; i < _entityInstallers.Length; i++)
                _entityInstallers[i].Install(this, world);
        }
        
        public void Dispose()
        {
            for (int i = 0; i < _entityInstallers.Length; i++)
                _entityInstallers[i].Uninstall(this, _world);
            
            if (Unpack(out int entityId))
                _world.DelEntity(entityId);

            _world = null;
            _packedEntity = default;
        }
        
        public bool IsAlive()
        {
            return Unpack(out int _);
        }
        
        public bool HasComponent<T>() where T : struct
        {
            if (!Unpack(out int entityId))
                throw new InvalidDataException("Failed to unpack entity");
            
            return _world.GetPool<T>().Has(entityId);
        }

        public bool Unpack(out int entityId)
        {
            return _packedEntity.Unpack(_world, out entityId);
        }
        
        public Entity AddComponent<T>(T component) where T : struct
        {
            if (!Unpack(out int entityId))
                return null;
            
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