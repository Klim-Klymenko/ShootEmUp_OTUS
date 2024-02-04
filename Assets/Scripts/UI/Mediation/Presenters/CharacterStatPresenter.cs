using System;
using GameSystem;
using UniRx;

namespace PM
{
    public sealed class CharacterStatPresenter : ICharacterStatPresenter, IDisposable
    {
        public event Action<ICharacterStatPresenter> OnValueUpdated;
        public string Value { get; set; }
        
        private readonly CharacterStat _characterStat;
        
        internal CharacterStatPresenter(CharacterStat characterStat)
        {
            _characterStat = characterStat;
            
            Value = $"{characterStat.Name}: {characterStat.Value}";
            
            _characterStat.OnValueChanged += UpdateValue;
        }
        
        private void UpdateValue(int value, string valueName)
        {
            Value = $"{valueName}: {value}";
            
            OnValueUpdated?.Invoke(this);
        }

        public void Dispose()
        {
            _characterStat.OnValueChanged -= UpdateValue;
        }
    }
}