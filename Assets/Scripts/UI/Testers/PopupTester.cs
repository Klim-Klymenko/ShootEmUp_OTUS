using UnityEngine;

namespace PM
{
    public sealed class PopupTester : MonoBehaviour
    {
        [SerializeField]
        private PopupManager _popupManager;

        [SerializeField]
        private string _name;

        [SerializeField]
        private string _description;

        [SerializeField]
        private Sprite _icon;

        [SerializeField]
        private int _experience;
        
        [SerializeField]
        private int _value;
        
        private MenuModel MenuModel => _popupManager.MenuModel;
        
        [ContextMenu("Change Name")]
        public void ChangeName()
        {
            foreach (var userInfo in MenuModel.GetUserInfos()) 
                userInfo.ChangeName(_name);
        }
        
        [ContextMenu("Change Description")]
        public void ChangeDescription()
        {
            foreach (var userInfo in MenuModel.GetUserInfos()) 
                userInfo.ChangeDescription(_description);
        }
        
        [ContextMenu("Change Icon")]
        public void ChangeIcon()
        {
            foreach (var userInfo in MenuModel.GetUserInfos()) 
                userInfo.ChangeIcon(_icon);
        }
        
        [ContextMenu("Add Experience")]
        public void AddExperience()
        {
            foreach (var playerLevel in MenuModel.GetPlayerLevels()) 
                playerLevel.AddExperience(_experience);
        }
        
        [ContextMenu("Change Value")]
        public void ChangeValue()
        {
            foreach (var characterInfo in MenuModel.GetCharactersInfos())
            {
                foreach (var stat in characterInfo.GetStats())
                    stat.ChangeValue(_value);
            }
        }

        [ContextMenu("Add Stat")]
        public void AddStat()
        {
            CharacterStat stat = new CharacterStat();
            
            foreach (var characterInfo in MenuModel.GetCharactersInfos())
                characterInfo.AddStat(stat);

            stat.Name = "Test";
            stat.ChangeValue(100);
        }
    }
}