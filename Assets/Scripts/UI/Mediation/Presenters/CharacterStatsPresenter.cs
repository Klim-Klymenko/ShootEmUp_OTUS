using System;
using System.Collections.Generic;
using GameSystem;
using JetBrains.Annotations;

namespace PM
{
    [UsedImplicitly]
    internal sealed class CharacterStatsPresenter : ICharacterStatsPresenter, IDisposable
    {
        public event Action<ICharacterStatPresenter> OnStatAdded;
        public event Action<ICharacterStatPresenter> OnStatRemoved;
        
        public List<ICharacterStatPresenter> InitialStatPresenters { get; } = new();

        private readonly Dictionary<CharacterStat, CharacterStatPresenter> _statPresenters = new();
        
        private readonly CharacterStats _characterStats;
        
        internal CharacterStatsPresenter(CharacterStats characterStats)
        {
            _characterStats = characterStats;
            
            foreach (CharacterStat characterStat in _characterStats.GetStats())
            {
                CharacterStatPresenter characterStatPresenter = new CharacterStatPresenter(characterStat);

                if (_statPresenters.TryAdd(characterStat, characterStatPresenter))
                    InitialStatPresenters.Add(characterStatPresenter);
            }
            
            _characterStats.OnStatAdded += CreateStatPresenter;
            _characterStats.OnStatRemoved += RemoveStatPresenter;
        }
        
        private void CreateStatPresenter(CharacterStat characterStat)
        {
            CharacterStatPresenter statPresenter = new CharacterStatPresenter(characterStat);
            
            OnStatAdded?.Invoke(statPresenter);
            
            _statPresenters.TryAdd(characterStat, statPresenter);
        }

        private void RemoveStatPresenter(CharacterStat characterStat)
        {
            CharacterStatPresenter statPresenter = _statPresenters[characterStat];
            
            OnStatRemoved?.Invoke(statPresenter);
            
            statPresenter.Dispose();
            _statPresenters.Remove(characterStat);
        }

        void IDisposable.Dispose()
        {
            _characterStats.OnStatAdded -= CreateStatPresenter;
            _characterStats.OnStatRemoved -= RemoveStatPresenter;
            
            foreach (CharacterStatPresenter characterStatPresenter in _statPresenters.Values)
                characterStatPresenter.Dispose();
        }
    }
}