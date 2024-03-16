using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Common
{
    [UsedImplicitly]
    public sealed class ServiceLocator
    {
        private readonly Dictionary<Type, object> _services = new();

        public void Bind<T>(T service) 
        {
            Type serviceType = typeof(T);
            
            if (_services.TryAdd(serviceType, service)) return;
            
            throw new ArgumentException("Service already exists");
        }
        
        public T Resolve<T>()
        {
            Type serviceType = typeof(T);
            
            if (_services.TryGetValue(serviceType, out object service))
                return (T) service;
            
            throw new ArgumentException("Service not found");
        }
    }
}