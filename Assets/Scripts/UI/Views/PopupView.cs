using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PM
{
    public sealed class PopupView : MonoBehaviour
    {
        [SerializeField]
        private Text _nameText;
        
        [SerializeField]
        private Text _descriptionText;
        
        [SerializeField]
        private Text _experienceText;
     
        [SerializeField]
        private Text _levelText;
        
        [SerializeField]
        private Text[] _valuesTexts;
        
        [SerializeField]
        private Image _avatar;
        
        [SerializeField]
        private Button _levelUpButton;
        
        [SerializeField]
        private Button _closeButton;
        
        private readonly Dictionary<string, Text> _statTexts = new();
        private int _lastStatTextIndex;
        
        private ICharacterPresenter _presenter;

        public void Construct(ICharacterPresenter presenter)
        {
            _presenter = presenter;
            
            Show();
        }

        private void Show()
        {
            UpdateName(_presenter.Name);
            UpdateDescription(_presenter.Description);
            UpdateAvatar(_presenter.Icon);
            UpdateValues(_presenter.Values, _presenter.ValuesNames);
            UpdateExperience(_presenter.Experience, _presenter.RequiredExperience);
            UpdateLevel(_presenter.Level);
            
            _levelUpButton.onClick.AddListener(_presenter.LevelUp);
            _levelUpButton.onClick.AddListener(UpdateLevelUpButton);
            _closeButton.onClick.AddListener(Hide);
        }
        
        private void Hide()
        {
            _levelUpButton.onClick.RemoveListener(_presenter.LevelUp);
            _levelUpButton.onClick.RemoveListener(UpdateLevelUpButton);
            _closeButton.onClick.RemoveListener(Hide);
        }
        
        private void UpdateLevelUpButton()
        {
            if (_presenter.CanLevelUp())
            {
                _levelUpButton.interactable = true;
                _levelUpButton.gameObject.SetActive(true);
            }
            else
            {
                _levelUpButton.interactable = false;
                _levelUpButton.gameObject.SetActive(false);
            }
        }
        
        public void UpdateName(string name) => _nameText.text = name;
        public void UpdateDescription(string description) => _descriptionText.text = description;
        public void UpdateAvatar(Sprite icon) => _avatar.sprite = icon;
        
        public void UpdateValues(int[] values, string[] valuesNames)
        {
            for (int i = 0; i < values.Length; i++)
            {
                _valuesTexts[i].text = $"{valuesNames[i]}: {values[i]}";
                _statTexts[valuesNames[i]] = _valuesTexts[i];
                _lastStatTextIndex = i;
            }
            
            int nextEmptyStatTextIndex = _lastStatTextIndex + 1;
            for (int i = nextEmptyStatTextIndex; i < _valuesTexts.Length; i++)
                _valuesTexts[i].text = string.Empty;
        }

        public void UpdateValue(int value, string valueName)
        {
            _statTexts[valueName].text = $"{valueName}: {value}";
        }
        
        public void InitializeValue(int value, string valueName)
        {
            int nextStatTextIndex = _lastStatTextIndex + 1;
            
            if (nextStatTextIndex >= _valuesTexts.Length) return;
            if (_valuesTexts[nextStatTextIndex] == null) return;
            
            _statTexts[valueName] = _valuesTexts[nextStatTextIndex];
            _statTexts[valueName].text = $"{valueName}: {value}";
            
            _lastStatTextIndex = nextStatTextIndex;
        }

        public void UpdateExperience(int experience, int requiredExperience)
        {
            _experienceText.text = $"{experience}/{requiredExperience}";
            
            UpdateLevelUpButton();
        }

        public void UpdateLevel(int level)
        {
            _levelText.text = $"Level: {level}";
        }
    }
}