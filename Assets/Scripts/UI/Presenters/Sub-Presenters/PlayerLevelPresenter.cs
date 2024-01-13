using System;

namespace PM
{
    public sealed class PlayerLevelPresenter : IDisposable
    {
        private readonly PlayerLevel _playerLevel;
        private PopupView _popupView;
        
        public PlayerLevelPresenter(PlayerLevel playerLevel)
        {
            _playerLevel = playerLevel;
        }
        
        public void Construct(PopupView popupView)
        {
            _popupView = popupView;
            
            _playerLevel.OnExperienceChanged += UpdateExperience;
            _playerLevel.OnLevelUp += UpdateLevel;
        }

        public void Dispose()
        {
            _playerLevel.OnExperienceChanged -= UpdateExperience;
            _playerLevel.OnLevelUp -= UpdateLevel;
        }
        
        private void UpdateExperience(int currentExperience, int requiredExperience)
        {
            _popupView.UpdateExperience(currentExperience, requiredExperience);
        }
        
        private void UpdateLevel(int level)
        {
            _popupView.UpdateLevel(level);
        }
    }
}