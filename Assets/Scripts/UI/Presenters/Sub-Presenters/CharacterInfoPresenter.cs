using System;
using System.Collections.Generic;
using UnityEngine;

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
        
        public void Dispose()
        {
            _characterInfo.OnStatAdded -= CreateStatPresenter;
            _characterInfo.OnStatRemoved -= RemoveStatPresenter;
            
            foreach (var characterStatPresenter in _characterStatPresenters.Values)
                characterStatPresenter.Dispose();
        }
        
        private void CreateStatPresenter(CharacterStat characterStat)
        {
            _characterStatPresenters.TryAdd(characterStat, new CharacterStatPresenter(characterStat, _popupView));
        }

        private void RemoveStatPresenter(CharacterStat characterStat)
        {
            _popupView.RemoveValue(characterStat.Name);
            
            CharacterStatPresenter statPresenter = _characterStatPresenters[characterStat];
            
            statPresenter.Dispose();
            _characterStatPresenters.Remove(characterStat);
        }
    }
}