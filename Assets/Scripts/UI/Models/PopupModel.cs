using SO;

namespace PM
{
    public sealed class PopupModel
    {
        public UserInfo UserInfo { get; private set; }
        public CharacterInfo CharacterInfo { get; private set; }
        public PlayerLevel PlayerLevel { get; private set; }
        public string[] ValueNames { get; }
        
        public PopupModel(PopupConfig config)
        {
            ValueNames = config.StatNames;
            
            CreateModels(config);
        }

        private void CreateModels(PopupConfig config)
        {
            UserInfo userInfo = new UserInfo();
            userInfo.ChangeDescription(config.Description);
            userInfo.ChangeIcon(config.Icon);
            userInfo.ChangeName(config.Name);
            UserInfo = userInfo;

            CharacterInfo characterInfo = new CharacterInfo();
            CharacterInfo = characterInfo;
            
            int[] values = config.Values;
            for (int i = 0; i < values.Length; i++)
            {
                CharacterStat stat = new CharacterStat();
                stat.ChangeValue(values[i]);
                stat.Name = config.StatNames[i];
                
                characterInfo.AddStat(stat);
            }
            
            PlayerLevel playerLevel = new PlayerLevel();
            PlayerLevel = playerLevel;
        }
    }
}