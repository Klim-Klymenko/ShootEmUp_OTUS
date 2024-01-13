using System;

namespace PM
{
    public sealed class CharacterStat
    {
        //для еще более мелекой вью и модели
        public event Action<int, string> OnValueChanged; 
        
        public string Name { get; set; }
        
        public int Value { get; private set; }

        public bool IsInitialized { get; private set; }

        public void ChangeValue(int value)
        {
            Value = value;
            OnValueChanged?.Invoke(value, Name);
            
            IsInitialized = true;
        }
    }
}