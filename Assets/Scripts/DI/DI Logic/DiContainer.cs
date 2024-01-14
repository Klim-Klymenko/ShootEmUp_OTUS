using UnityEngine;

namespace ShootEmUp
{
    public sealed class DiContainer
    {
        private readonly ServiceLocator _serviceLocator;
        private readonly Injector _injector;
        
        internal DiContainer(ServiceLocator serviceLocator, Injector injector)
        {
            _serviceLocator = serviceLocator;
            _injector = injector;
        }

        public void BindService(object service) => _serviceLocator.BindService(service);
        
        public T Resolve<T>() where T : class => _serviceLocator.GetService<T>();
        
        public void Inject(object target)
        {
            _injector.Inject(target, _serviceLocator);
        }

        public T Instantiate<T>(T prefab) where T : Object
        {
            T instance = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
            _injector.Inject(instance, _serviceLocator);
            return instance;
        }
        
        public T Instantiate<T>(T prefab, Vector3 position, Quaternion rotation) where T : Object
        {
            T instance = Object.Instantiate(prefab, position, rotation);
            _injector.Inject(instance, _serviceLocator);
            return instance;
        }
        
        public T Instantiate<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent) where T : Object
        {
            T instance = Object.Instantiate(prefab, position, rotation, parent);
            _injector.Inject(instance, _serviceLocator);
            return instance;
        }
    }
}
