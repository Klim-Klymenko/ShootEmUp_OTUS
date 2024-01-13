using System.Collections.Generic;
using SO;

namespace PM
{
    public sealed class MenuModel
    {
        private readonly List<PopupModel> _models = new();
        public List<PopupModel> Models => _models;

        public string[] this[int index] => _models[index].ValuesNames;
        
        public MenuModel(PopupConfigCollection configCollection)
        {
            CreateModels(configCollection.PopupConfigs);
        }

        private void CreateModels(IReadOnlyList<PopupConfig> configs)
        {
            for (int i = 0; i < configs.Count; i++)
                CreateModel(configs[i]);
        }
        
        public PopupModel CreateModel(PopupConfig config)
        {
            PopupModel popupModel = new PopupModel(config);
            _models.Add(popupModel);
            
            return popupModel;
        }
        
        public List<UserInfo> GetUserInfos()
        {
            List<UserInfo> userInfos = new();
            
            for (int i = 0; i < _models.Count; i++)
                userInfos.Add(_models[i].UserInfo);

            return userInfos;
        }
        
        public List<PlayerLevel> GetPlayerLevels()
        {
            List<PlayerLevel> playerLevels = new();
            
            for (int i = 0; i < _models.Count; i++)
                playerLevels.Add(_models[i].PlayerLevel);

            return playerLevels;
        }
        
        public List<CharacterInfo> GetCharactersInfos()
        {
            List<CharacterInfo> characterInfos = new();
            
            for (int i = 0; i < _models.Count; i++)
                characterInfos.Add(_models[i].CharacterInfo);
            
            return characterInfos;
        }
    }
}