using System.Collections.Generic;

namespace PM
{
    public sealed class MenuPresenter : IMenuPresenter
    {
        private readonly List<PopupPresenter> _popupPresenters = new();
        public IReadOnlyList<PopupPresenter> PopupPresenters => _popupPresenters;
        
        public MenuPresenter(IReadOnlyList<PopupModel> popupModels)
        {
            CreatePresenters(popupModels);
        }

        public void Construct(IReadOnlyList<PopupView> popupViews)
        {
            for (int i = 0; i < _popupPresenters.Count; i++)
                _popupPresenters[i].Construct(popupViews[i]);
        }
        
        private void CreatePresenters(IReadOnlyList<PopupModel> popupModels)
        {
            for (int i = 0; i < popupModels.Count; i++)
                CreatePresenter(popupModels[i]);
        }

        public PopupPresenter CreatePresenter(PopupModel popupModel)
        {
            PopupPresenter popupPresenter = new PopupPresenter(popupModel.ValuesNames,
                popupModel.UserInfo, popupModel.PlayerLevel, popupModel.CharacterInfo);
                
            _popupPresenters.Add(popupPresenter);

            return popupPresenter;
        }
        
        public void DestroyPresenter(PopupPresenter popupPresenter)
        {
            popupPresenter.DestroyPresenters();
            _popupPresenters.Remove(popupPresenter);
        }
        
        public void DestroyPresenters()
        {
            for (int i = 0; i < _popupPresenters.Count; i++)
                DestroyPresenter(_popupPresenters[i]);
        }

        public void DestroyLastPresenter() => DestroyPresenter(_popupPresenters[^1]);
        
        public IReadOnlyList<CharacterPresenter> GetCharacterPresenters()
        {
            List<CharacterPresenter> characterPresenters = new();

            for (int i = 0; i < _popupPresenters.Count; i++)
                characterPresenters.Add(_popupPresenters[i].CharacterPresenter);

            return characterPresenters;
        }
    }
}