using System.Collections.Generic;
using Factory;
using JetBrains.Annotations;
using UnityEngine;

namespace Pool
{
    [UsedImplicitly]
    public sealed class Pool<TPrefab> where TPrefab : class, new()
    {
        private readonly List<TPrefab> _spawnedEntities = new();
        
        private readonly int _reservationAmount;
        private readonly Transform _container;
        private readonly IFactory<TPrefab> _factory;
        
        public Pool(IFactory<TPrefab> factory, int reservationAmount, Transform container)
        {
            _factory = factory;
            _reservationAmount = reservationAmount;
            _container = container;
            
            Reserve();
        }
        
        private void Reserve()
        {
            for (int i = 0; i < _reservationAmount; i++)
            {
                TPrefab entity = _factory.Create();

                if (entity is MonoBehaviour monoBehaviour)
                {
                    monoBehaviour.gameObject.SetActive(false);
                    monoBehaviour.transform.SetParent(_container);
                }
                
                _spawnedEntities.Add(entity);
            }
        }
        
        public TPrefab Get()
        {
            if (_spawnedEntities.Count == 0)
                Reserve();
           
            TPrefab entity = _spawnedEntities[^1];
            _spawnedEntities.Remove(entity);
            
            if (entity is MonoBehaviour monoBehaviour)
                monoBehaviour.gameObject.SetActive(true);
            
            return entity;
        }
        
        public void Put(TPrefab entity)
        {
            if (entity is MonoBehaviour monoBehaviour)
            {
                monoBehaviour.gameObject.SetActive(false);
                monoBehaviour.transform.SetParent(_container);
            }
            
            _spawnedEntities.Add(entity);
        }
    }
}