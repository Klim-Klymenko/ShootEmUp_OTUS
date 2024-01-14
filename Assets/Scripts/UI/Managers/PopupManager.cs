using System.Collections.Generic;
using Adapters;
using JetBrains.Annotations;
using SO;
using UnityEngine;

namespace PM
{
    [UsedImplicitly]
    public sealed class PopupManager
    {
        public MenuModel MenuModel { get; private set; }
        private MenuPresenter _menuPresenter;
        private MenuView _menuView;
        
        private MenuAdapter _menuAdapter;

        private readonly Factory.IFactory<MenuView> _menuViewFactory;
        private readonly PopupConfigCollection _configCollection;

        public PopupManager(Factory.IFactory<MenuView> menuViewFactory, PopupConfigCollection configCollection)
        {
            _menuViewFactory = menuViewFactory;
            _configCollection = configCollection;
        }

        public void Show()
        {
            MenuModel = new MenuModel();
            _menuPresenter = new MenuPresenter();
            _menuView = _menuViewFactory.Create();
            
            _menuAdapter = new MenuAdapter(_menuView, _menuPresenter, MenuModel, _configCollection);
            
            for (int i = 0; i < _configCollection.Count; i++)
                _menuView.ShowPopup();
        }
        
        public void Hide()
        {
            IReadOnlyList<PopupView> popupViews = _menuView.PopupViews;
            
            for (int i = 0; i < popupViews.Count; i++)
                _menuView.DestroyPopup(popupViews[i]);
            
            _menuAdapter.Dispose();
            Object.Destroy(_menuView.gameObject);
        }

        public void AddPopup() => _menuView.ShowPopup();
        
        public void RemoveLastPopup() => _menuView.HideLastPopup();
    }
}