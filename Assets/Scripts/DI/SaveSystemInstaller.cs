using UnityEngine;
using Zenject;

namespace SaveSystem
{
    public sealed class SaveSystemInstaller : MonoInstaller
    {
        [SerializeField]
        private SaveLoadManager _saveLoadManager;
        
        [SerializeField]
        private SceneContext _sceneContext;
        
        private void Construct()
        {
            Container.Inject(_saveLoadManager);
        }

        public override void InstallBindings()
        {
            BindManagers();
            BindRepository();
            
            SubscribeConstruct();
        }

        private void OnDestroy()
        {
            UnsubscribeConstruct();
        }
        
        private void BindRepository()
        {
            Container.BindInterfacesTo<Repository>().AsCached();
        }

        private void BindManagers()
        {
            Container.Bind<SaveLoadManager>().FromInstance(_saveLoadManager).AsSingle();
        }
        
        private void SubscribeConstruct() => _sceneContext.PostInstall += Construct;
        private void UnsubscribeConstruct() => _sceneContext.PostInstall -= Construct;
    }
}