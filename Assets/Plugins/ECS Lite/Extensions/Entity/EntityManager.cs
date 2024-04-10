using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common;
using JetBrains.Annotations;
using Leopotam.EcsLite;
using UnityEngine;
using Object = UnityEngine.Object;

namespace EcsEngine.Extensions
{
    [UsedImplicitly]
    public sealed class EntityManager
    {
        private EcsWorld _world;
        private Entity[] _sceneEntities;

        private readonly Dictionary<string, Pool> _pools = new();
        private readonly Dictionary<int, Entity> _entities = new();

        public EntityManager(Pool[] pools)
        {
            for (int i = 0; i < pools.Length; i++)
            {
                Pool pool = pools[i];
              
                if (!_pools.TryAdd(pool.PrefabName, pool))
                    throw new ArgumentException("Pool with the same prefab type already exists");
            }
        }
        
        public void Initialize(EcsWorld world)
        {
            _world = world;
            _sceneEntities = Object.FindObjectsOfType<Entity>();
           
            for (int i = 0; i < _sceneEntities.Length; i++)
            {
                Entity entity = _sceneEntities[i];
                
                if (!entity.InstallOnAwake) continue;
                
                entity.Initialize(world);
                
                if (!entity.Unpack(out int entityId))
                    throw new Exception("Failed to unpack entity");
     
                _entities.Add(entityId, entity);
            }
        }

        public Entity CreateEntity(Entity prefab, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            Pool pool = GetPool(prefab);
            Entity entity = pool.Get();
            
            Transform entityTransform = entity.transform;
            
            entityTransform.position = position;
            entityTransform.rotation = rotation;
            
            if (parent != null)
                entityTransform.parent = parent;
            
            entity.Initialize(_world);
            
            if (entity.Unpack(out int entityId))
                _entities.Add(entityId, entity);
                
            return entity;
        }
        
        public Entity CreateEntity(Entity prefab, Vector3 position, Quaternion rotation, out int entityId, Transform parent = null)
        {
            Pool pool = GetPool(prefab);
            Entity entity = pool.Get();
            
            Transform entityTransform = entity.transform;
            
            entityTransform.position = position;
            entityTransform.rotation = rotation;
            
            if (parent != null)
                entityTransform.parent = parent;
            
            entity.Initialize(_world);
            
            if (entity.Unpack(out entityId))
                _entities.Add(entityId, entity);
                
            return entity;
        }
        
        public void Destroy(int entityId)
        {
            if (!_entities.Remove(entityId, out Entity entity)) return;
        
            Pool pool = GetPool(entity);
            
            entity.Dispose();
            pool.Put(entity);
        }

        public Entity GetEntity(int entityId)
        {
            if (_entities.TryGetValue(entityId, out Entity entity))
                return entity;

            throw new InvalidDataException("Entity is not found");
        }

        private Pool GetPool(Entity prefab)
        {
            if (!_pools.TryGetValue(prefab.Name, out Pool pool))
                throw new ArgumentException("Pool with the specified type does not exist");

            return pool;
        }
    }
}