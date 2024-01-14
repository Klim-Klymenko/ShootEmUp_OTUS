using System;
using System.Collections.Generic;
using UnityEngine;

namespace PM
{
    public sealed class CharacterInfoPresenter : IDisposable
    {
        private readonly Dictionary<CharacterStat, CharacterStatPresenter> _characterStatPresenters = new();
        
        private readonly CharacterInfo _characterInfo;
        private readonly PopupView _popupView;
        
        public CharacterInfoPresenter(CharacterInfo characterInfo, PopupView popupView)
        {
            _characterInfo = characterInfo;
            _popupView = popupView;
            
            foreach (var characterStat in _characterInfo.GetStats())
                _characterStatPresenters.TryAdd(characterStat, new CharacterStatPresenter(characterStat, _popupView));
            
            _characterInfo.OnStatAdded += CreateStatPresenter;
            _characterInfo.OnStatRemoved += RemoveStatPresenter;
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
        
        public void Dispose()
        {
            _characterInfo.OnStatAdded -= CreateStatPresenter;
            _characterInfo.OnStatRemoved -= RemoveStatPresenter;
            
            foreach (var characterStatPresenter in _characterStatPresenters.Values)
                characterStatPresenter.Dispose();
        }
    }
}