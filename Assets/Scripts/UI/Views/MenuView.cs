using System;
using System.Collections.Generic;
using Pool;
using UnityEngine;

namespace PM
{
    public sealed class MenuView : MonoBehaviour
    {
        public event Func<PopupView, ICharacterPresenter> OnPopupShown; 
        public event Action <PopupView> OnPopupDestroyed;
        
        [SerializeField]
        private Transform _contentContainer;
        
        private readonly List<PopupView> _popupViews = new();
        public List<PopupView> PopupViews => _popupViews;

        private Pool<PopupView> _viewPool;
        
        public void Construct(Pool<PopupView> viewPool)
        {
            _viewPool = viewPool;
        }
        
        public void ShowPopup()
        {
            PopupView popupView = _viewPool.Get();
            
            ICharacterPresenter characterPresenter = OnPopupShown?.Invoke(popupView);
            
            popupView.Construct(characterPresenter, () => DestroyPopup(popupView));
            popupView.transform.SetParent(_contentContainer);
            
            _popupViews.Add(popupView);
        }
        
        public void DestroyPopup(PopupView popupView)
        {
            _viewPool.Put(popupView);
            _popupViews.Remove(popupView);
            
            OnPopupDestroyed?.Invoke(popupView);
        }
        
        public void HideLastPopup() => DestroyPopup(_popupViews[^1]);
    }
}