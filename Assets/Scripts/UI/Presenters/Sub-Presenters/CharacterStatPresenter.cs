using System;

namespace PM
{
    public sealed class CharacterStatPresenter : IDisposable
    {
        private readonly CharacterStat _characterStat;
        private readonly PopupView _popupView;
        
        public CharacterStatPresenter(CharacterStat characterStat, PopupView popupView)
        {
            _characterStat = characterStat;
            _popupView = popupView;
            
            _characterStat.OnValueChanged += UpdateValue;
        }

        public void Dispose()
        {
            _characterStat.OnValueChanged -= UpdateValue;
        }
        
        private void UpdateValue(int value, string valueName)
        {
            if (_characterStat.IsInitialized)
                _popupView.UpdateValue(value, valueName);
            else
                _popupView.InitializeValue(value, valueName);
        }
    }
}