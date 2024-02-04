using System;

namespace PM
{
    public interface ICharacterStatPresenter
    {
        event Action<ICharacterStatPresenter> OnValueUpdated;
        
        string Value { get; }
    }
}