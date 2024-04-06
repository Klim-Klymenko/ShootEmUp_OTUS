using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common;
using JetBrains.Annotations;
using Leopotam.EcsLite;
using UnityEngine;

namespace EcsEngine.Extensions
{
    [UsedImplicitly]
    public sealed class EntityManager
    {
        private EcsWorld _world;
        
        private readonly Dictionary<Type, Pool> _pools = new();
        private readonly Dictionary<int, Entity> _entities = new();
        private readonly Entity[] _sceneEntities;

        public EntityManager(Pool[] pools, Entity[] sceneEntities)
        {
            _sceneEntities = sceneEntities;
            
            for (int i = 0; i < pools.Length; i++)
            {
                Pool pool = pools[i];
                Type prefabType = pool.PrefabType;
                
                if (!_pools.TryAdd(prefabType, pool))
                    throw new ArgumentException("Pool with the same prefab type already exists");
            }
        }
        
        public void Initialize(EcsWorld world)
        {
            _world = world;
            
            for (int i = 0; i < _sceneEntities.Length; i++)
            {
                Entity entity = _sceneEntities[i];
                entity.Initialize(world);
                
                if (!entity.Unpack(out int entityId))
                    throw new Exception("Failed to unpack entity");
     
                _entities.Add(entityId, entity);
            }
        }

        public Entity CreateEntity(Entity prefab, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            Pool pool = GetPool(prefab);
            MonoBehaviour obj = pool.Get();

            Entity entity = obj.GetComponent<Entity>();
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

        private Pool GetPool(Component prefab)
        {
            Type prefabType = null;
            Type[] prefabsTypes = _pools.Keys.ToArray();

            for (int i = 0; i < prefabsTypes.Length; i++)
            {
                Type currentPrefabType = prefabsTypes[i];

                if (ContainsType(currentPrefabType, prefab))
                {
                    prefabType = currentPrefabType;
                    break;
                }
            }

            if (prefabType == null)
                throw new NullReferenceException("Pool of prefab you want to spawn is not found");
            
            if (!_pools.TryGetValue(prefabType, out Pool pool))
                throw new ArgumentException("Pool with the specified type does not exist");

            return pool;
        }
        
        private bool ContainsType(Type desiredType, Component entity)
        {
            MonoBehaviour[] entityComponents = entity.GetComponents<MonoBehaviour>();

            for (int i = 0; i < entityComponents.Length; i++)
            {
                Type componentType = entityComponents[i].GetType();

                if (desiredType == componentType)
                    return true;
            }
            
            return false;
        }
    }
}