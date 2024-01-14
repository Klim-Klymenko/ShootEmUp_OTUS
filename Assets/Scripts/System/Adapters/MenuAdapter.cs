using System;
using System.Collections.Generic;
using PM;
using SO;

namespace Adapters
{
    public sealed class MenuAdapter : IDisposable
    {
        private readonly PopupConfigCollection _configCollection;
        
        private readonly MenuView _menuView;
        private readonly MenuPresenter _menuPresenter; 
        private readonly MenuModel _menuModel;

        private PopupView _currentPopupView;
        
        private readonly Dictionary<PopupView, PopupModel> _popupModels = new();
        private readonly Dictionary<PopupView, PopupPresenter> _popupPresenters = new();
        
        public MenuAdapter(MenuView menuView, MenuPresenter menuPresenter, MenuModel menuModel, PopupConfigCollection configCollection)
        {
            _configCollection = configCollection;
            
            _menuView = menuView;
            _menuPresenter = menuPresenter;
            _menuModel = menuModel;
            
            IReadOnlyList<PopupView> popupViews = _menuView.PopupViews;
            IReadOnlyList<PopupPresenter> popupPresenters = _menuPresenter.PopupPresenters;
            IReadOnlyList<PopupModel> popupModels = _menuModel.PopupModels;

            for (int i = 0; i < popupViews.Count; i++)
            {
                _popupModels.TryAdd(popupViews[i], popupModels[i]);
                _popupPresenters.TryAdd(popupViews[i], popupPresenters[i]);
            }
            
            _menuView.OnPopupDestroyed += DestroyPopupPresenter;
            _menuView.OnPopupDestroyed += DestroyPopupModel;

            _menuView.OnPopupShown += CreatePopupPresenter;
            _menuPresenter.OnPresenterCreated += CreatePopupModel;
        }

        private void DestroyPopupPresenter(PopupView view)
        {
            _menuPresenter.DestroyPresenter(_popupPresenters[view]);
            
            _popupPresenters.Remove(view);
        }
        
        private void DestroyPopupModel(PopupView view)
        {
            _menuModel.DestroyPopupModel(_popupModels[view]);
            
            _popupModels.Remove(view);
        }

        private ICharacterPresenter CreatePopupPresenter(PopupView popupView)
        {
            _currentPopupView = popupView;
            
            PopupPresenter popupPresenter = _menuPresenter.CreatePresenter(popupView);
            _popupPresenters.TryAdd(popupView, popupPresenter);

            return popupPresenter.CharacterPresenter;
        }
        
        private PopupModel CreatePopupModel()
        {
            PopupModel popupModel = _menuModel.CreatePopupModel(_configCollection.GetPopupConfig());
            _popupModels.TryAdd(_currentPopupView, popupModel);
            
            return popupModel;
        }
        
        public void Dispose()
        {
            _menuView.OnPopupDestroyed -= DestroyPopupPresenter;
            _menuView.OnPopupDestroyed -= DestroyPopupModel;
            
            _menuView.OnPopupShown -= CreatePopupPresenter;
            _menuPresenter.OnPresenterCreated -= CreatePopupModel;
        }
    }
}