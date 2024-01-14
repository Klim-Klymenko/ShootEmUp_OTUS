using System.Collections.Generic;
using Adapters;
using SO;
using UnityEngine;
using Zenject;

namespace PM
{
    public sealed class PopupManager : MonoBehaviour
    {
        [SerializeField]
        private PopupConfigCollection _configCollection;
        
        private Factory.IFactory<MenuView> _menuViewFactory;
        
        public MenuModel MenuModel { get; private set; }
        private MenuPresenter _menuPresenter;
        private MenuView _menuView;
        
        private MenuAdapter _menuAdapter;

        [Inject]
        public void Construct(Factory.IFactory<MenuView> menuViewFactory)
        {
            _menuViewFactory = menuViewFactory;
        }

        [ContextMenu("Show")]
        public void Show()
        {
            MenuModel = new MenuModel();
            _menuPresenter = new MenuPresenter();
            _menuView = _menuViewFactory.Create();
            
            _menuAdapter = new MenuAdapter(_menuView, _menuPresenter, MenuModel, _configCollection);
            
            for (int i = 0; i < _configCollection.Count; i++)
                _menuView.ShowPopup();
        }
        
        [ContextMenu("Hide")]
        public void Hide()
        {
            IReadOnlyList<PopupView> popupViews = _menuView.PopupViews;
            
            for (int i = 0; i < popupViews.Count; i++)
                _menuView.DestroyPopup(popupViews[i]);
            
            _menuAdapter.Dispose();
            Destroy(_menuView.gameObject);
        }

        [ContextMenu("Add New Popup")]
        public void AddPopup()
        {
            _menuView.ShowPopup();
        }

        [ContextMenu("Remove Last Popup")]
        public void RemoveLastPopup()
        {
            _menuView.HideLastPopup();
        }
    }
}