using System;
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
        private int _lastDisplayedStatTextIndex;
        private int FirstEmptyStatTextIndex => _lastDisplayedStatTextIndex + 1;
        
        private ICharacterPresenter _presenter;
        
        private Action _closeAction;

        public void Construct(ICharacterPresenter presenter, Action closeAction)
        {
            _presenter = presenter;
            _closeAction = closeAction;
            
            Show();
        }

        private void Show()
        {
            UpdateName(_presenter.Name);
            UpdateDescription(_presenter.Description);
            UpdateAvatar(_presenter.Icon);
            UpdateValues(_presenter.Values, _presenter.ValueNames);
            UpdateExperience(_presenter.Experience, _presenter.RequiredExperience);
            UpdateLevel(_presenter.Level);
            
            _levelUpButton.onClick.AddListener(_presenter.LevelUp);
            _levelUpButton.onClick.AddListener(UpdateLevelUpButton);
            _closeButton.onClick.AddListener(Hide);
        }
        
        private void Hide()
        {
            _closeAction();
            _statTexts.Clear();
            
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
        
        private void UpdateValues(IReadOnlyList<int> values, IReadOnlyList<string> valueNames)
        {
            for (int i = 0; i < values.Count; i++)
            {
                _statTexts[valueNames[i]] = _valuesTexts[i];
                UpdateValue(values[i], valueNames[i]);
                _lastDisplayedStatTextIndex = i;
            }
            
            for (int i = FirstEmptyStatTextIndex; i < _valuesTexts.Length; i++)
                _valuesTexts[i].text = string.Empty;
        }

        public void UpdateValue(int value, string valueName)
        {
            if (!_statTexts.ContainsKey(valueName)) return;
            
            _statTexts[valueName].text = $"{valueName}: {value}";
        }
        
        public void InitializeValue(int value, string valueName)
        {
            if (FirstEmptyStatTextIndex >= _valuesTexts.Length) return;
            if (_valuesTexts[FirstEmptyStatTextIndex] == null) return;
            
            _statTexts[valueName] = _valuesTexts[FirstEmptyStatTextIndex];
            UpdateValue(value, valueName);
            
            _lastDisplayedStatTextIndex = FirstEmptyStatTextIndex;
        }

        public void RemoveValue(string valueName)
        {
            if (!_statTexts.ContainsKey(valueName)) return;

            _valuesTexts[_lastDisplayedStatTextIndex].text = string.Empty;
            _statTexts.Remove(valueName);

            _lastDisplayedStatTextIndex--;
        }
        
        public void UpdateExperience(int experience, int requiredExperience)
        {
            _experienceText.text = $"{experience}/{requiredExperience}";
            
            UpdateLevelUpButton();
        }

        public void UpdateLevel(int level) => _levelText.text = $"Level: {level}";
    }
}