using System;
using System.Collections.Generic;

namespace PM
{
    public sealed class MenuPresenter
    {
        public event Func<PopupModel> OnPopupPresenterCreated; 
        
        private readonly List<PopupPresenter> _popupPresenters = new();
        public IReadOnlyList<PopupPresenter> PopupPresenters => _popupPresenters;
        
        public PopupPresenter CreatePresenter(PopupView popupView)
        {
            PopupModel popupModel = OnPopupPresenterCreated?.Invoke();

            if (popupModel == null) throw new NullReferenceException("PopupModel is null when creating presenter");
            
            PopupPresenter popupPresenter = new PopupPresenter(popupModel.ValuesNames,
                popupModel.UserInfo, popupModel.PlayerLevel, popupModel.CharacterInfo);
            
            popupPresenter.Construct(popupView);    
            _popupPresenters.Add(popupPresenter);

            return popupPresenter;
        }
        
        public void DestroyPresenter(PopupPresenter popupPresenter)
        {
            popupPresenter.DestroyPresenters();
            _popupPresenters.Remove(popupPresenter);
        }
        
        public IReadOnlyList<CharacterPresenter> GetCharacterPresenters()
        {
            List<CharacterPresenter> characterPresenters = new();

            for (int i = 0; i < _popupPresenters.Count; i++)
                characterPresenters.Add(_popupPresenters[i].CharacterPresenter);

            return characterPresenters;
        }
    }
}