using Factory;
using PM;
using UnityEngine;
using Zenject;
using Pool;

namespace DI
{
    public sealed class PopupInstaller : MonoInstaller
    {
        [SerializeField]
        private MenuView _menuViewPrefab;
        
        [SerializeField]
        private PopupView _popupViewPrefab;

        [SerializeField]
        private Transform _popupPanel;

        [SerializeField]
        private Transform _viewPoolContainer;
        
        [SerializeField]
        private int _poolSize;
        
        public override void InstallBindings()
        {
            BindFactory();
            BindPool();
        }

        private void BindFactory()
        {
            Container.BindInterfacesTo<MenuViewFactory>().AsCached().WithArguments(_menuViewPrefab, _popupPanel);
        }

        private void BindPool()
        {
            Factory.IFactory<PopupView> popupViewFactory = new PopupViewFactory(_popupViewPrefab, _popupPanel);
            Container.Bind<Pool<PopupView>>().AsSingle().WithArguments(popupViewFactory, _poolSize, _viewPoolContainer);
        }
    }
}