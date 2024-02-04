using GameSystem;
using SO;
using UnityEngine;
using Zenject;

namespace PM
{
    internal class PopupTester : MonoBehaviour
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
        private UserInfo _userInfo;
        private PlayerLevel _playerLevel;
        private CharacterStats _characterStats;
        
        [Inject]
        internal void Construct(PopupManager popupManager, UserInfo userInfo, PlayerLevel playerLevel, CharacterStats characterStats)
        {
            _popupManager = popupManager;
            _userInfo = userInfo;
            _playerLevel = playerLevel;
            _characterStats = characterStats;
        }
        
        [ContextMenu("Show Popup")]
        internal void ShowPopup()
        {
            _popupManager.Show();
        }
        
        [ContextMenu("Hide Popup")]
        internal void HidePopup()
        {
            _popupManager.Hide();
        }
        
        [ContextMenu("Change Name")]
        internal void ChangeName()
        {
            _userInfo.ChangeName(_name);
        }
        
        [ContextMenu("Change Description")]
        internal void ChangeDescription()
        {
            _userInfo.ChangeDescription(_description);
        }
        
        [ContextMenu("Change Icon")]
        internal void ChangeIcon()
        {
            _userInfo.ChangeIcon(_icon);
        }
        
        [ContextMenu("Add Experience")]
        internal void AddExperience()
        {
            _playerLevel.AddExperience(_experience);
        }
        
        [ContextMenu("Change Value")]
        internal void ChangeValue()
        {
            foreach (CharacterStat stat in _characterStats.GetStats())
                stat.ChangeValue(_value);
        }

        [ContextMenu("Add Stat")]
        internal void AddStat()
        {
            CharacterStat stat = new CharacterStat();
            
            _characterStats.AddStat(stat);

            stat.Name = $"Test {_createdStatsAmount}";
            stat.ChangeValue(100);

            _createdStatsAmount++;
        }
        
        [ContextMenu("Remove Stat")]
        internal void RemoveStat()
        {
            _characterStats.RemoveStat(_characterStats.GetStats()[^1]);
        }
    }
}