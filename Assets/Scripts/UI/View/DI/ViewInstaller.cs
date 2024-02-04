using Common;
using UnityEngine;
using Zenject;

namespace PM
{
    internal sealed class ViewInstaller : MonoInstaller
    {
        [SerializeField]
        private CharacterView _characterViewPrefab;

        [SerializeField]
        private Transform _popupPanel;
        
        [SerializeField]
        private Transform _viewPoolContainer;
        
        [SerializeField]
        private int _poolSize;
        
        public override void InstallBindings()
        {
            BindPool();
            BindSpawner();
            BindPopupManager();
        }
        
        private void BindPool()
        {
            Common.IFactory<CharacterView> popupViewFactory = new CharacterViewFactory(_characterViewPrefab, _popupPanel, Container);
            Container.Bind<Pool<CharacterView>>().AsSingle().WithArguments(popupViewFactory, _poolSize, _viewPoolContainer);
        }

        private void BindSpawner()
        {
            Container.Bind<ISpawner<CharacterView>>().To<CharacterViewSpawner>().AsSingle().WithArguments(_popupPanel);
        }
        
        private void BindPopupManager()
        {
            Container.Bind<PopupManager>().AsSingle();
        }
    }
}