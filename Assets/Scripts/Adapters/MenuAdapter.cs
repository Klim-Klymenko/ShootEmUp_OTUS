using System;
using System.Collections.Generic;
using PM;

namespace Adapters
{
    public sealed class MenuAdapter : IDisposable
    {
        private readonly MenuView _menuView;
        private readonly MenuPresenter _menuPresenter; 
        private readonly MenuModel _menuModel;
        
        private readonly Dictionary<PopupView, PopupModel> _popupModels = new();
        private readonly Dictionary<PopupView, PopupPresenter> _popupPresenters = new();
        
        public MenuAdapter(MenuView menuView, MenuPresenter menuPresenter, MenuModel menuModel)
        {
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
        }

        private void DestroyPopupPresenter(PopupView view)
        {
            _menuPresenter.DestroyPresenter(_popupPresenters[view]);
        }
        
        private void DestroyPopupModel(PopupView view)
        {
            _menuModel.DestroyModel(_popupModels[view]);
        }
        
        public void Dispose()
        {
            _menuView.OnPopupDestroyed -= DestroyPopupPresenter;
            _menuView.OnPopupDestroyed -= DestroyPopupModel;
        }
    }
}