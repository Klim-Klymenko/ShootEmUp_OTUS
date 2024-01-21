using UnityEngine;
using Zenject;

namespace SaveSystem
{
    internal sealed class SaveSystemInstaller : MonoInstaller
    {
        [SerializeField]
        private SaveLoadManager _saveLoadManager;
       
        public override void InstallBindings()
        {
            BindManagers();
            BindRepository();
        }
        
        private void BindRepository()
        {
            Container.BindInterfacesTo<Repository>().AsCached();
        }

        private void BindManagers()
        {
            Container.Bind<SaveLoadManager>().FromMethod(InjectSaveLoadManager).AsSingle().NonLazy();
        }

        private SaveLoadManager InjectSaveLoadManager()
        {
            Container.Inject(_saveLoadManager);
            return _saveLoadManager;
        }
    }
}