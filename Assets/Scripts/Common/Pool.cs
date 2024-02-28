using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Common
{
    [UsedImplicitly]
    public sealed class Pool<T> where T : Object
    {
        private readonly List<T> _objects;
        
        private readonly int _reservationAmount;
        private readonly T _prefab;
        private readonly Vector3 _spawnPosition;
        private readonly Quaternion _spawnRotation;
        private readonly Transform _parent;
        
        public Pool(int reservationAmount, T prefab, Transform parent)
        {
            _objects = new List<T>(reservationAmount);
            
            _reservationAmount = reservationAmount;
            _prefab = prefab;
            _spawnPosition = Vector3.zero;
            _spawnRotation = Quaternion.identity;
            _parent = parent;
            
            Reserve();
        }
        
        private void Reserve()
        {
            for (int i = 0; i < _reservationAmount; i++)
            {
                T obj = Object.Instantiate(_prefab, _spawnPosition, _spawnRotation, _parent);
            
                _objects.Add(obj);
                SetActive(obj, false);
            }
        }

        public T Get()
        {
            T obj = _objects.Count > 0 ? _objects[^1] : Object.Instantiate(_prefab, _spawnPosition, _spawnRotation, _parent);
            
            SetActive(obj, true);
            _objects.Remove(obj);
            
            return obj;
        }

        public void Put(T obj)
        {
            SetActive(obj, false);
            _objects.Add(obj);
            
            if (_parent != null)
                SetParent(obj);
        }
        
        private void SetActive(T obj, bool value)
        {
            if (obj is MonoBehaviour monoBehaviour)
                monoBehaviour.gameObject.SetActive(value);
                
            else if (obj is GameObject gameObject)
                gameObject.SetActive(value);
        }

        private void SetParent(T obj)
        {
            if (obj is MonoBehaviour monoBehaviour)
            {
                Transform transform = monoBehaviour.transform;
                
                InstallParent(transform);
            }
                
                
            else if (obj is GameObject gameObject)
            {
                Transform transform = gameObject.transform;
                
                InstallParent(transform);
            }
            
            else if (obj is Transform transform)
            {
                InstallParent(transform);
            }

            return;

            void InstallParent(Transform transform)
            {
                if (transform.parent != _parent)
                    transform.SetParent(_parent);
            }
        }
    }
}