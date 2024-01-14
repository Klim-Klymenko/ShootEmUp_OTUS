using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    internal sealed class ServiceLocator
    {
        private readonly Dictionary<Type, object> _services = new();

        internal T GetService<T>() where T : class
        {
            Type serviceType = typeof(T);

            if (_services.TryGetValue(serviceType, out object service)) 
                return service as T;
        
            throw new Exception($"There is no element you want to access in the service locator: {serviceType.Name}");
        }
    
        internal object GetService(Type serviceType)
        {
            if (_services.TryGetValue(serviceType, out object service)) 
                return service;
            
            throw new Exception($"There is no element you want to access in the service locator: {serviceType.Name}");
        }
        
        internal bool TryGetService(Type serviceType, out object service)
        {
            if (_services.TryGetValue(serviceType, out object locatorService))
            {
                service = locatorService;
                return true;
            }

            service = null;
            return false;
        }
        
        internal void BindService(object service)
        {
            Type elementType = service.GetType();
            _services.TryAdd(elementType, service);
        }
        
        internal void BindService(Type interfaceType, object service)
        {
            _services.TryAdd(interfaceType, service);
        }
    }
}