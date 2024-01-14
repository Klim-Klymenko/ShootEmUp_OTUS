using UnityEngine;

namespace PM
{
    public sealed class CharacterPresenter : ICharacterPresenter
    {
        public string Name { get; }
        public string[] ValueNames { get; }
        public string Description { get; }
        public Sprite Icon { get; }
        public int[] Values { get; }
        public int Experience { get; }
        public int RequiredExperience { get; }
        public int Level { get; }
        
        private readonly PlayerLevel _playerLevel;
        
        public CharacterPresenter(string[] valueNames, UserInfo userInfo, PlayerLevel playerLevel, CharacterInfo characterInfo)
        {
            Name = userInfo.Name;
            Description = userInfo.Description;
            Icon = userInfo.Icon;
            Experience = playerLevel.CurrentExperience;
            RequiredExperience = playerLevel.RequiredExperience;
            Level = playerLevel.CurrentLevel;
            
            ValueNames = valueNames;
            Values = new int[valueNames.Length];
            
            for (int i = 0; i < ValueNames.Length; i++)
                Values[i] = characterInfo.GetStat(ValueNames[i]).Value;
            
            _playerLevel = playerLevel;
        }
        
        void ICharacterPresenter.LevelUp() => _playerLevel.LevelUp();
        
        bool ICharacterPresenter.CanLevelUp() =>  _playerLevel.CanLevelUp();
    }
}