using System;
using JetBrains.Annotations;

namespace GameSystem
{
    [UsedImplicitly]
    public sealed class CharacterStat
    {
        public event Action<int, string> OnValueChanged; 
        
        public string Name { get; set; }
        
        public int Value { get; private set; }

        public void ChangeValue(int value)
        {
            Value = value;
            OnValueChanged?.Invoke(value, Name);
        }
    }
}