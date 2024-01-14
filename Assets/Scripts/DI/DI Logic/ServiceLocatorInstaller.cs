using System;
using System.Collections.Generic;

namespace ShootEmUp
{
    internal sealed class ServiceLocatorInstaller
    {
        private readonly ServiceLocator _serviceLocator;
        
        internal ServiceLocatorInstaller(ServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }
        
        internal void InstallServices(SystemInstallableArgs args, IEnumerable<Installer> installers)
        {
            _serviceLocator.BindService(args.DiContainer);
            _serviceLocator.BindService(args.GameManager);
            
            foreach (var installer in installers)
                BindServices(installer);
        }
        
        internal void InstallServices(DiContainer diContainer, IEnumerable<Installer> dependencyInstallers)
        {
            _serviceLocator.BindService(diContainer);
            
            foreach (var installer in dependencyInstallers)
                BindServices(installer);
        }
        
        private void BindServices(Installer installer)
        {
            foreach (object service in installer.ProvideServices())
            {
                _serviceLocator.BindService(service);

                Type[] interfacesType = service.GetType().GetInterfaces();
                
                for (int i = 0; i < interfacesType.Length; i++)
                {
                    if (installer.InterfacesType.Contains(interfacesType[i])) 
                        _serviceLocator.BindService(interfacesType[i], service);
                }
            }
        }
    }
}