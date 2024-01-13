using System.Collections.Generic;
using Pool;
using UnityEngine;

namespace PM
{
    public sealed class MenuView : MonoBehaviour
    {
        [SerializeField]
        private Transform _contentContainer;
        
        private readonly Dictionary<PopupView, PopupPresenter> _popupPresenters = new();
        private readonly Dictionary<PopupView, PopupModel> _popupModels = new();
        
        private readonly List<PopupView> _popupViews = new();
        public List<PopupView> PopupViews => _popupViews;

        private Pool<PopupView> _viewPool;
        
        private IMenuPresenter _menuPresenter;
        private IMenuModel _menuModel;
        
        private IReadOnlyList<CharacterPresenter> CharacterPresenters => _menuPresenter.GetCharacterPresenters();
        private IReadOnlyList<PopupPresenter> PopupPresenters => _menuPresenter.PopupPresenters;
        private IReadOnlyList<PopupModel> PopupModels => _menuModel.PopupModels;
        
        public void Construct(Pool<PopupView> viewPool, IMenuPresenter menuPresenter, IMenuModel menuModel)
        {
            _viewPool = viewPool;
            _menuPresenter = menuPresenter;
            _menuModel = menuModel;
            
            ShowPopups();
        }
        
        private void ShowPopups()
        {
            for (int i = 0; i < CharacterPresenters.Count; i++)
                ShowPopup(PopupPresenters[i], PopupModels[i]);
        }

        public PopupView ShowPopup(PopupPresenter popupPresenter, PopupModel popupModel)
        {
            PopupView view = _viewPool.Get();
            view.Construct(popupPresenter.CharacterPresenter, () => DestroyPopup(view));
            view.transform.SetParent(_contentContainer);
            
            _popupViews.Add(view);
            _popupPresenters.TryAdd(view, popupPresenter);
            _popupModels.TryAdd(view, popupModel);
            
            return view;
        }
        
        private void DestroyPopup(PopupView view)
        {
            HidePopup(view);
            
            _menuPresenter.DestroyPresenter(_popupPresenters[view]);
            _menuModel.DestroyModel(_popupModels[view]);
            
            if (_popupPresenters.ContainsKey(view))
                _popupModels.Remove(view);
            
            if (_popupModels.ContainsKey(view))
                _popupModels.Remove(view);
        }
        
        private void HidePopup(PopupView view)
        {
            _viewPool.Put(view);
            _popupViews.Remove(view);
        }
        
        public void HidePopups()
        {
            for (int i = 0; i < _popupViews.Count; i++)
                HidePopup(_popupViews[i]);
            
            Destroy(gameObject);
        }

        public void HideLastPopup() => HidePopup(_popupViews[^1]);
    }
}