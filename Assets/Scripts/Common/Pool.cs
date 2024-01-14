using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ShootEmUp
{
    public sealed class Pool<T> where T : Object, IPoolable
    {
        private readonly int _initialAmount;
        private readonly T _prefab;
        private readonly Transform _parentToGet;
        private readonly Transform _parentToPut;
        private readonly List<T> _entities = new();

        public Pool(int reservationAmount, T prefab, Transform parentToGet, Transform parentToPut)
        {
            _initialAmount = reservationAmount;
            _prefab = prefab;
            _parentToGet = parentToGet;
            _parentToPut = parentToPut;
        }

        public void Reserve()
        {
            for (var i = 0; i < _initialAmount; i++)
            {
                T currentEntity = Object.Instantiate(_prefab, _parentToPut);
                
                currentEntity.GameObject.SetActive(false);

                _entities.Add(currentEntity);
            }
        }

        public List<T> Get(int entitiesAmount)
        {
            if (entitiesAmount > _entities.Count)
                throw new Exception("Not enough bullets in the pool");
            
            List<T> entityToGet = new();

            for (int i = _entities.Count - 1; entitiesAmount > 0; entitiesAmount--, i--)
            {
                T currentEntity = _entities[i];

                currentEntity.GameObject.SetActive(true);
                currentEntity.Transform.SetParent(_parentToGet);
                
                entityToGet.Add(currentEntity);
                _entities.Remove(currentEntity);
            }

            return entityToGet;
        }

        public T Get()
        {
            T currentEntity = _entities[^1];

            currentEntity.GameObject.SetActive(true);
            currentEntity.Transform.SetParent(_parentToGet);
            
            _entities.Remove(currentEntity);
            
            return currentEntity;
        }

        public void Put(List<T> entitiesToPut)
        {
            for (int i = 0; i < entitiesToPut.Count; i++)
            {
                T currentBullet = entitiesToPut[i];

                ResetPosition(currentBullet.Transform);
                
                currentBullet.Transform.SetParent(_parentToPut);
                currentBullet.GameObject.SetActive(false);
                
                _entities.Add(currentBullet);
            }

            entitiesToPut.Clear();
        }

        public void Put(T entityToPut)
        {
            ResetPosition(entityToPut.Transform);
            
            entityToPut.Transform.SetParent(_parentToPut);
            entityToPut.GameObject.SetActive(false);
            
            _entities.Add(entityToPut);
        }

        private void ResetPosition(Transform transform) => transform.position = Vector3.zero;
    }
}

