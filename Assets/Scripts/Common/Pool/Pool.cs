using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Common
{
    [UsedImplicitly]
    public sealed class Pool 
    {
        private readonly List<MonoBehaviour> _objects;

        private readonly int _reservationAmount;
        private readonly MonoBehaviour _prefab;
        private readonly Vector3 _spawnPosition;
        private readonly Quaternion _spawnRotation;
        private readonly Transform _parent;

        public Type PrefabType { get; }

        public Pool(int reservationAmount, MonoBehaviour prefab, Transform parent)
        {
            _objects = new List<MonoBehaviour>(reservationAmount);
            
            _reservationAmount = reservationAmount;
            _prefab = prefab;
            _spawnPosition = Vector3.zero;
            _spawnRotation = Quaternion.identity;
            _parent = parent;
            
            PrefabType = prefab.GetType();
            
            Reserve();
        }

        private void Reserve()
        {
            for (int i = 0; i < _reservationAmount; i++)
            {
                MonoBehaviour obj = Object.Instantiate(_prefab, _spawnPosition, _spawnRotation, _parent);
            
                _objects.Add(obj);
                obj.gameObject.SetActive(false);
            }
        }

        public MonoBehaviour Get()
        {
            MonoBehaviour obj = _objects.Count > 0 ? _objects[^1] : Object.Instantiate(_prefab, _spawnPosition, _spawnRotation, _parent);
            
            obj.gameObject.SetActive(true);
            _objects.Remove(obj);
            
            return obj;
        }

        public void Put(MonoBehaviour obj)
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