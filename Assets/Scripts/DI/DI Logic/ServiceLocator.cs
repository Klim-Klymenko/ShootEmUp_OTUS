using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Debug = UnityEngine.Debug;

namespace ShootEmUp
{
    public sealed class ServiceLocator
    {
        private readonly Dictionary<Type, object> _services = new();

        public void InstallServices(SystemInstallablesArgs args, IEnumerable<DependencyInstaller> dependencyInstallers)
        {
            BindService(args.DependencyAssembler);
            BindService(args.GameManager);
            
            foreach (var installer in dependencyInstallers)
                BindServices(installer);
        }
        
        public void InstallServices(DependencyAssembler dependencyAssembler, IEnumerable<DependencyInstaller> dependencyInstallers)
        {
            BindService(dependencyAssembler);
            
            foreach (var installer in dependencyInstallers)
                BindServices(installer);
        }
        
        public T GetService<T>() where T : class
        {
            Type serviceType = typeof(T);

            if (_services.ContainsKey(serviceType)) 
                return _services[serviceType] as T;
        
            throw new Exception($"There is no element you want to access in the service locator: {serviceType.Name}");
        }
    
        public object GetService(Type serviceType)
        {
            if (_services.ContainsKey(serviceType)) 
                return _services[serviceType];
        
            throw new Exception($"There is no element you want to access in the service locator: {serviceType.Name}");
        }

        public void BindService(object service)
        {
            Type elementType = service.GetType();
            if (!_services.ContainsKey(elementType))
            {
                _services.Add(elementType, service);
            }
        }
        private void BindServiceWithExplicitType(object service, Type interfaceType)
        {
            if (!_services.ContainsKey(interfaceType))
                _services.Add(interfaceType, service);
        }
        
        private void BindServices(DependencyInstaller dependencyInstaller)
        {
            foreach (var service in dependencyInstaller.ProvideServices())
            {
                BindService(service);

                Type[] interfacesType = service.GetType().GetInterfaces();
                
                for (int i = 0; i < interfacesType.Length; i++)
                {
                    if (dependencyInstaller.InterfacesType.Contains(interfacesType[i])) 
                        BindServiceWithExplicitType(service, interfacesType[i]);
                }
            }
        }
    }
}