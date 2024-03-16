using System;
using System.Collections.Generic;
using Common;
using JetBrains.Annotations;
using Leopotam.EcsLite;
using UnityEngine;

namespace EcsEngine
{
    [UsedImplicitly]
    public sealed class EntityManager
    {
        private const int PrefabTypeIndex = 0;

        private EcsWorld _world;
        
        private readonly Dictionary<Type, object> _pools = new();
        private readonly Dictionary<int, Entity> _entities;
        private readonly Entity[] _sceneEntities;

        public EntityManager(object[] pools, Entity[] sceneEntities)
        {
            _sceneEntities = sceneEntities;
            
            for (int i = 0; i < pools.Length; i++)
            {
                object pool = pools[i];
                Type prefabType = pool.GetType().GetGenericArguments()[PrefabTypeIndex];
                
                if (!_pools.TryAdd(prefabType, pool))
                    throw new ArgumentException("Pool with the same type already exists");
            }
        }
        
        public void Initialize(EcsWorld world)
        {
            _world = world;
//TODO: понять как ассоциировать энтити с их пулами
            for (int i = 0; i < _sceneEntities.Length; i++)
            {
                Entity entity = _sceneEntities[i];
                entity.Initialize(_world);
                
                if (!entity.PackedEntity.Unpack(world, out int entityId))
                    throw new Exception("Failed to unpack entity");
                
                _entities.Add(entityId, entity);
            }
        }

        public Entity CreateEntity<T>(T prefab) where T : MonoBehaviour
        {
            Type prefabType = typeof(T);
            
            if (!_pools.TryGetValue(prefabType, out object pool))
                throw new ArgumentException("Pool with the specified type does not exist");
            
            Pool<T> castedPool = (Pool<T>) pool;
            T obj = castedPool.Get();

            Entity entity = obj.GetComponent<Entity>();
            entity.Initialize(_world);
                
            return entity;
        }
        
        public void Destroy(int entityId)
        {
            Entity entity = _entities[entityId];
            
            
        }
    }
}