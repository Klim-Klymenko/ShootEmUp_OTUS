using System.Collections.Generic;
using Pool;
using UnityEngine;

namespace PM
{
    public sealed class MenuView : MonoBehaviour
    {
        [SerializeField]
        private Transform _contentContainer;
        
        private readonly List<PopupView> _views = new();
        public List<PopupView> Views => _views;

        private Pool<PopupView> _viewPool;
        
        public void Construct(List<CharacterPresenter> characterPresenters, Pool<PopupView> viewPool)
        {
            _viewPool = viewPool;
            
            ShowPopups(characterPresenters);
        }
        
        private void ShowPopups(IReadOnlyList<CharacterPresenter> characterPresenters)
        {
            for (int i = 0; i < characterPresenters.Count; i++)
            {
                PopupView view = ShowPopup(characterPresenters[i]);
                _views.Add(view);
            }
        }

        public PopupView ShowPopup(CharacterPresenter characterPresenter)
        {
            PopupView view = _viewPool.Get();
            view.Construct(characterPresenter);
            view.transform.SetParent(_contentContainer);
            
            return view;
        }
        
        public void HidePopups()
        {
            _viewPool.Put(_views);
            _views.Clear();
            
            Destroy(gameObject);
        }

        public void HideLastPopup()
        {
            PopupView view = _views[^1];
            
            _viewPool.Put(view);
            _views.Remove(view);
        }
    }
}