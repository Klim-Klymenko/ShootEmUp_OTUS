using System;
using System.Collections.Generic;

namespace PM
{
    public sealed class CharacterInfoPresenter : IDisposable
    {
        private readonly Dictionary<CharacterStat, CharacterStatPresenter> _characterStatPresenters = new();
        
        private readonly CharacterInfo _characterInfo;
        private PopupView _popupView;
        
        public CharacterInfoPresenter(CharacterInfo characterInfo)
        {
            _characterInfo = characterInfo;
        }

        public void Construct(PopupView popupView)
        {
            _popupView = popupView;
            
            foreach (var characterStat in _characterInfo.GetStats())
                _characterStatPresenters.TryAdd(characterStat, new CharacterStatPresenter(characterStat, _popupView));
            
            _characterInfo.OnStatAdded += CreateStatPresenter;
            _characterInfo.OnStatRemoved += RemoveStatPresenter;
        }
        
        void IDisposable.Dispose()
        {
            _characterInfo.OnStatAdded -= CreateStatPresenter;
            _characterInfo.OnStatRemoved -= RemoveStatPresenter;
        }
        
        private void CreateStatPresenter(CharacterStat characterStat)
        {
            _characterStatPresenters.TryAdd(characterStat, new CharacterStatPresenter(characterStat, _popupView));
        }

        private void RemoveStatPresenter(CharacterStat characterStat)
        {
            CharacterStatPresenter statPresenter = _characterStatPresenters[characterStat];
            
            statPresenter.Dispose();
            _characterStatPresenters.Remove(characterStat);
        }
    }
}