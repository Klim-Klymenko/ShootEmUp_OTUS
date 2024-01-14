using System.Collections.Generic;
using SO;

namespace PM
{
    public sealed class MenuModel
    {
        private readonly List<PopupModel> _popupModels = new();
        public IReadOnlyList<PopupModel> PopupModels => _popupModels;
        
        public PopupModel CreatePopupModel(PopupConfig config)
        {
            PopupModel popupModel = new PopupModel(config);
            _popupModels.Add(popupModel);
            
            return popupModel;
        }
        
        public void DestroyPopupModel(PopupModel popupModel)
        {
            _popupModels.Remove(popupModel);
        }
        
        public List<UserInfo> GetUserInfos()
        {
            List<UserInfo> userInfos = new();
            
            for (int i = 0; i < _popupModels.Count; i++)
                userInfos.Add(_popupModels[i].UserInfo);

            return userInfos;
        }
        
        public List<PlayerLevel> GetPlayerLevels()
        {
            List<PlayerLevel> playerLevels = new();
            
            for (int i = 0; i < _popupModels.Count; i++)
                playerLevels.Add(_popupModels[i].PlayerLevel);

            return playerLevels;
        }
        
        public List<CharacterInfo> GetCharactersInfos()
        {
            List<CharacterInfo> characterInfos = new();
            
            for (int i = 0; i < _popupModels.Count; i++)
                characterInfos.Add(_popupModels[i].CharacterInfo);
            
            return characterInfos;
        }
    }
}