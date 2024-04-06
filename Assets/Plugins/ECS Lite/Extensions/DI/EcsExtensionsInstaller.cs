using Zenject;

namespace EcsEngine.Extensions
{
    internal sealed class EcsExtensionsInstaller : MonoInstaller 
    {
        public override void InstallBindings()
        {
            BindEcsEntityBuilder();
        }
        
        private void BindEcsEntityBuilder()
        {
            Container.Bind<EcsEntityBuilder>().AsCached();
        }
    }
}