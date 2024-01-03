using UnityEngine;
using Zenject;

namespace Domain
{
    public class DomainInstaller : MonoInstaller
    {
        [SerializeField]
        private UnitsCatalog _catalog;
        
        public override void InstallBindings()
        {
            BindSaveLoaders();
            BindUnitsCatalog();
        }
        
        private void BindSaveLoaders()
        {
            Container.BindInterfacesTo<UnitSaveLoader>().AsCached();
            Container.BindInterfacesTo<ResourcesSaveLoader>().AsCached();
        }
        
        private void BindUnitsCatalog()
        {
            Container.Bind<UnitsCatalog>().FromInstance(_catalog).AsSingle();
        }
    }
}