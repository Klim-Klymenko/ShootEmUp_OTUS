using System.Collections.Generic;
using EcsEngine.Extensions;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Common
{
    [UsedImplicitly]
    public sealed class Pool 
    {
        private readonly List<Entity> _objects;

        private readonly int _poolSize;
        private readonly Entity _prefab;
        private readonly Transform _parent;
        private readonly DiContainer _diContainer;
        
        public readonly string PrefabName;

        public Pool(int poolSize, Entity prefab, Transform parent, DiContainer diContainer)
        {
            _objects = new List<Entity>(poolSize);
            
            _poolSize = poolSize;
            _prefab = prefab;
            _parent = parent;
            _diContainer = diContainer;

            PrefabName = prefab.Name;
        }

        public void Reserve()
        {
            for (int i = 0; i < _poolSize; i++)
            {
                Entity obj = _diContainer.InstantiatePrefab(_prefab, _parent).GetComponent<Entity>();
            
                _objects.Add(obj);
                obj.gameObject.SetActive(false);
            }
        }

        public Entity Get()
        {
            Entity obj = _objects.Count > 0 ? _objects[^1] : _diContainer.InstantiatePrefab(_prefab, _parent).GetComponent<Entity>();
            
            obj.gameObject.SetActive(true);
            _objects.Remove(obj);
            
            return obj;
        }

        public void Put(Entity obj)
        {
            obj.gameObject.SetActive(false);
            SetParent(obj.transform);
            
            _objects.Add(obj);
        }
        
        private void SetParent(Transform transform)
        {
            if (_parent == null) return;
                
            if (transform.parent != _parent) 
                transform.SetParent(_parent);
        }    
    }
}