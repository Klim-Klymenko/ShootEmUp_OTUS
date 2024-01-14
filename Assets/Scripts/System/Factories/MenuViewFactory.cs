using JetBrains.Annotations;
using PM;
using Pool;
using UnityEngine;

namespace Factory
{
    [UsedImplicitly]
    public sealed class MenuViewFactory : IFactory<MenuView>
    {
        private readonly MenuView _menuViewPrefab;
        private readonly Transform _popupPanel;
        private readonly Pool<PopupView> _popupViewPool;
        
        public MenuViewFactory(MenuView menuViewPrefab, Transform popupPanel, Pool<PopupView> popupViewPool)
        {
            _menuViewPrefab = menuViewPrefab;
            _popupPanel = popupPanel;
            _popupViewPool = popupViewPool;
        }

        MenuView IFactory<MenuView>.Create()
        {
            MenuView menuView = Object.Instantiate(_menuViewPrefab, _popupPanel);
            menuView.Construct(_popupViewPool);

            return menuView;
        }
    }
}