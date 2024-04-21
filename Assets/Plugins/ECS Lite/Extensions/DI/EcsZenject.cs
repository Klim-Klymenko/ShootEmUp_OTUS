using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace EcsEngine.Extensions
{
    public struct EcsZenject<T> : IEcsDataInject
    {
        public T Value { get; private set; }
        
        void IEcsDataInject.Fill(IEcsSystems systems)
        {
            Value = systems.GetShared<DiContainer>().Resolve<T>();
        }
    }
}