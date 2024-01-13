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
        private PopupView _popupViewPrefab;

        [SerializeField]
        private Transform _popupPanel;

        [SerializeField]
        private Transform _viewPoolContainer;
        
        [SerializeField]
        private int _poolSize;
        
        private PopupViewFactory _popupViewFactory;
        
        public override void InstallBindings()
        {
            BindFactory();
            BindPool();
        }

        private void BindFactory()
        {
            _popupViewFactory = new PopupViewFactory(_popupViewPrefab, _popupPanel);
            Container.Bind<PopupViewFactory>().FromInstance(_popupViewFactory).AsSingle();
        }

        private void BindPool()
        {
            Container.Bind<Pool<PopupView>>().AsSingle().WithArguments(_popupViewFactory, _poolSize, _viewPoolContainer);
        }
    }
}