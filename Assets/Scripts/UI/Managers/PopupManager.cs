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
            MenuModel = new MenuModel(_configCollection);
            
            _menuPresenter = new MenuPresenter(MenuModel);
            
            _menuView = Instantiate(_menuViewPrefab, _popupPanel);
            
            _menuView.Construct(_menuPresenter.GetCharacterPresenters(), _viewPool);
            _menuPresenter.Construct(_menuView.Views);
        }
        
        [ContextMenu("Hide")]
        public void Hide()
        {
            _menuView.HidePopups(); 
            Destroy(_menuView.gameObject);
        }

        [ContextMenu("Add New Popup")]
        public void AddPopup()
        {
            PopupModel model = MenuModel.CreateModel(_configCollection.PopupConfigs[0]);
            PopupPresenter presenter = _menuPresenter.CreatePresenter(model);
            PopupView view = _menuView.ShowPopup(presenter.CharacterPresenter);
            
            presenter.Construct(view);
        }
        
        [ContextMenu("Remove Last Popup")]
        public void RemovePopup()
        {
            _menuView.HideLastPopup();
        }
    }
}