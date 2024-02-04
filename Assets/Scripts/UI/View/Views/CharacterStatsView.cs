using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PM
{
    internal sealed class CharacterStatsView : MonoBehaviour
    {
        [SerializeField]
        private Text[] _valuesTexts;
        
        private readonly Dictionary<ICharacterStatPresenter, Text> _statTexts = new();
        
        private int _lastDisplayedStatTextIndex = -1;
        private int FirstEmptyStatTextIndex => _lastDisplayedStatTextIndex + 1;

        private ICharacterStatsPresenter _presenter;
        
        [Inject]
        internal void Construct(ICharacterStatsPresenter presenter)
        {
            _presenter = presenter;
        }

        internal void Show()
        {
            InitializeValues(_presenter.InitialStatPresenters);
            
            _presenter.OnStatAdded += InitializeValue;
            _presenter.OnStatRemoved += RemoveValue;
        }

        internal void Hide()
        {
            _statTexts.Clear();
            _lastDisplayedStatTextIndex = -1;
            
            _presenter.OnStatAdded -= InitializeValue;
            _presenter.OnStatRemoved -= RemoveValue;
        }
        
        private void InitializeValues(IReadOnlyList<ICharacterStatPresenter> statPresenters)
        {
            for (int i = 0; i < statPresenters.Count; i++)
                InitializeValue(statPresenters[i]);
            
            for (int i = FirstEmptyStatTextIndex; i < _valuesTexts.Length; i++)
                _valuesTexts[i].text = string.Empty;
        }

        private void UpdateValue(ICharacterStatPresenter statPresenter)
        {
            if (!_statTexts.ContainsKey(statPresenter)) return;
            
            _statTexts[statPresenter].text = statPresenter.Value;
        }
        
        private void InitializeValue(ICharacterStatPresenter statPresenter)
        {
            if (FirstEmptyStatTextIndex >= _valuesTexts.Length) return;
            
            Text valueText = _valuesTexts[FirstEmptyStatTextIndex];
            
            if (valueText == null) return;
            
            _statTexts[statPresenter] = valueText;
            UpdateValue(statPresenter);

            statPresenter.OnValueUpdated += UpdateValue;
            
            _lastDisplayedStatTextIndex = FirstEmptyStatTextIndex;
        }

        private void RemoveValue(ICharacterStatPresenter statPresenter)
        {
            if (!_statTexts.ContainsKey(statPresenter)) return;

            _statTexts[statPresenter].text = string.Empty;
            _statTexts.Remove(statPresenter);

            _lastDisplayedStatTextIndex--;
            
            statPresenter.OnValueUpdated -= UpdateValue;
        }
    }
}