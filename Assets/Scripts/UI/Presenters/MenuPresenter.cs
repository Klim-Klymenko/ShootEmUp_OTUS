using System;
using System.Collections.Generic;

namespace PM
{
    public sealed class MenuPresenter
    {
        private readonly List<PopupPresenter> _presenters = new();
        
        public MenuPresenter(MenuModel menuModel)
        {
            CreatePresenters(menuModel);
        }

        public void Construct(List<PopupView> popupViews)
        {
            for (int i = 0; i < _presenters.Count; i++)
                _presenters[i].Construct(popupViews[i]);
        }
        
        private void CreatePresenters(MenuModel menuModel)
        {
            List<PopupModel> models = menuModel.Models;

            for (int i = 0; i < models.Count; i++)
                CreatePresenter(models[i]);
        }

        public PopupPresenter CreatePresenter(PopupModel popupModel)
        {
            PopupPresenter presenter = new PopupPresenter(popupModel.ValuesNames,
                popupModel.UserInfo, popupModel.PlayerLevel, popupModel.CharacterInfo);
                
            _presenters.Add(presenter);

            return presenter;
        }
        
        public List<CharacterPresenter> GetCharacterPresenters()
        {
            List<CharacterPresenter> characterPresenters = new();

            for (int i = 0; i < _presenters.Count; i++)
                characterPresenters.Add(_presenters[i].CharacterPresenter);

            return characterPresenters;
        }
    }
}