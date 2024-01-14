using System.Collections.Generic;
using SO;

namespace PM
{
    public sealed class MenuModel
    {
        private readonly List<PopupModel> _popupModels = new();
        public IReadOnlyList<PopupModel> PopupModels => _popupModels;
        
        public MenuModel(IReadOnlyList<PopupConfig> configs)
        {
            CreateModels(configs);
        }

        private void CreateModels(IReadOnlyList<PopupConfig> configs)
        {
            for (int i = 0; i < configs.Count; i++)
                CreatePopupModel(configs[i]);
        }
        
        public PopupModel CreatePopupModel(PopupConfig config)
        {
            PopupModel popupModel = new PopupModel(config);
            _popupModels.Add(popupModel);
            
            return popupModel;
        }
        
        public void DestroyModel(PopupModel popupModel)
        {
            _popupModels.Remove(popupModel);
        }
        
        public void DestroyModels()
        {
            for (int i = 0; i < _popupModels.Count; i++)
                DestroyModel(_popupModels[i]);
        }
        
        public void DestroyLastModel() => DestroyModel(_popupModels[^1]);
        
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