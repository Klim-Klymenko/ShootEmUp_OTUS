using Pool;
using SO;
using UnityEngine;
using Zenject;

namespace PM
{
    public sealed class PopupManager : MonoBehaviour
    {
        [SerializeField]
        private Transform _popupPanel;
        
        [SerializeField]
        private MenuView _menuViewPrefab;
        
        [SerializeField]
        private PopupConfigCollection _configCollection;
        
        private Pool<PopupView> _viewPool;

        public MenuModel MenuModel { get; private set; }
        private MenuPresenter _menuPresenter;
        private MenuView _menuView;

        [Inject]
        public void Construct(Pool<PopupView> viewPool)
        {
            _viewPool = viewPool;  
        }

        [ContextMenu("Show")]
        public void Show()
        {
            MenuModel = new MenuModel(_configCollection.PopupConfigs);
            
            _menuPresenter = new MenuPresenter(MenuModel.PopupModels);
            
            _menuView = Instantiate(_menuViewPrefab, _popupPanel);
            
            _menuView.Construct(_viewPool, _menuPresenter, MenuModel);
            _menuPresenter.Construct(_menuView.PopupViews);
        }

        [ContextMenu("Hide")]
        public void Hide()
        {
            MenuModel.DestroyModels();
            _menuPresenter.DestroyPresenters();
            _menuView.HidePopups();
            
            Destroy(_menuView.gameObject);
        }

        [ContextMenu("Add New Popup")]
        public void AddPopup()
        {
            PopupModel popupModel = MenuModel.CreatePopupModel(_configCollection.PopupConfigs[0]);
            PopupPresenter popupPresenter = _menuPresenter.CreatePresenter(popupModel);
            PopupView popupView = _menuView.ShowPopup(popupPresenter, popupModel);
            
            popupPresenter.Construct(popupView);
        }

        [ContextMenu("Remove Last Popup")]
        public void RemoveLastPopup()
        {
            MenuModel.DestroyLastModel();
            _menuPresenter.DestroyLastPresenter();
            _menuView.HideLastPopup();
        }
    }
}