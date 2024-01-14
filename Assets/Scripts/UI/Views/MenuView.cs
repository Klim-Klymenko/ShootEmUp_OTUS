using System;
using System.Collections.Generic;
using Pool;
using UnityEngine;

namespace PM
{
    public sealed class MenuView : MonoBehaviour
    {
        public event Action <PopupView> OnPopupDestroyed;
        
        [SerializeField]
        private Transform _contentContainer;
        
        private readonly List<PopupView> _popupViews = new();
        public List<PopupView> PopupViews => _popupViews;

        private Pool<PopupView> _viewPool;
        
        public void Construct(Pool<PopupView> viewPool, IReadOnlyList<CharacterPresenter> characterPresenters)
        {
            _viewPool = viewPool;
            
            ShowPopups(characterPresenters);
        }
        
        private void ShowPopups(IReadOnlyList<CharacterPresenter> characterPresenters)
        {
            for (int i = 0; i < characterPresenters.Count; i++)
                ShowPopup(characterPresenters[i]);
        }

        public PopupView ShowPopup(CharacterPresenter characterPresenter)
        {
            PopupView view = _viewPool.Get();
            view.Construct(characterPresenter, () => DestroyPopup(view));
            view.transform.SetParent(_contentContainer);
            
            _popupViews.Add(view);
            
            return view;
        }
        
        private void DestroyPopup(PopupView view)
        {
            HidePopup(view);
            
            OnPopupDestroyed?.Invoke(view);
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