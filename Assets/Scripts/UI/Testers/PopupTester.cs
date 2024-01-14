using UnityEngine;
using Zenject;

namespace PM
{
    public sealed class PopupTester : MonoBehaviour
    {
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

        private int _createdStatsAmount;
        
        private PopupManager _popupManager;
        private MenuModel MenuModel => _popupManager.MenuModel;
        
        [Inject]
        public void Construct(PopupManager popupManager)
        {
            _popupManager = popupManager;
        }
        
        [ContextMenu("Show Popups")]
        public void ShowPopups()
        {
            _popupManager.Show();
        }
        
        [ContextMenu("Hide Popups")]
        public void HidePopups()
        {
            _popupManager.Hide();
        }
        
        [ContextMenu("Add Popup")]
        public void AddPopup()
        {
            _popupManager.AddPopup();
        }
        
        [ContextMenu("Remove Last Popup")]
        public void RemoveLastPopup()
        {
            _popupManager.RemoveLastPopup();
        }
        
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

            stat.Name = $"Test {_createdStatsAmount}";
            stat.ChangeValue(100);

            _createdStatsAmount++;
        }
        
        [ContextMenu("Remove Stat")]
        public void RemoveStat()
        {
            foreach (var characterInfo in MenuModel.GetCharactersInfos())
                characterInfo.RemoveStat(characterInfo.GetStats()[^1]);
        }
    }
}