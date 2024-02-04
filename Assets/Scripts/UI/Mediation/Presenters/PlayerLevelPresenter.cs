using System;
using GameSystem;
using JetBrains.Annotations;
using UniRx;

namespace PM
{
    [UsedImplicitly]
    internal sealed class PlayerLevelPresenter : IPlayerLevelPresenter, IDisposable
    {
       public Action OnClosed { get; }
       
       void IPlayerLevelPresenter.LevelUp() => _playerLevel.LevelUp();
       bool IPlayerLevelPresenter.CanLevelUp() => _playerLevel.CanLevelUp();
       
        IReadOnlyReactiveProperty<string> IPlayerLevelPresenter.Experience => _experience;
        IReadOnlyReactiveProperty<string> IPlayerLevelPresenter.Level => _level;

        private readonly ReactiveProperty<string> _experience;
        private readonly ReactiveProperty<string> _level;

        private readonly PlayerLevel _playerLevel;
        
        internal PlayerLevelPresenter(PlayerLevel playerLevel)
        {
            _playerLevel = playerLevel;
            
            _experience = new ReactiveProperty<string>($"{_playerLevel.CurrentExperience}/{_playerLevel.RequiredExperience}");
            _level = new ReactiveProperty<string>($"Level:{_playerLevel.CurrentLevel.ToString()}");
            
            _playerLevel.OnExperienceChanged += UpdateExperience;
            _playerLevel.OnLevelUp += UpdateLevel;

            OnClosed += _playerLevel.ResetExperience;
        }
        
        private void UpdateExperience(int currentExperience, int requiredExperience)
        {
            _experience.Value = $"{currentExperience}/{requiredExperience}";
        }
        
        private void UpdateLevel(int level)
        {
            _level.Value = $"Level:{level.ToString()}";
        }

        void IDisposable.Dispose()
        {
            _playerLevel.OnExperienceChanged -= UpdateExperience;
            _playerLevel.OnLevelUp -= UpdateLevel;
        }
    }
}