using System;
using JetBrains.Annotations;

namespace GameSystem
{
    [UsedImplicitly]
    public sealed class PlayerLevel
    {
        public event Action<int> OnLevelUp;
        public event Action<int, int> OnExperienceChanged;
        
        public int CurrentLevel { get; private set; } = 1;

        public int CurrentExperience { get; private set; }
        
        public int RequiredExperience => 100 * (CurrentLevel + 1);
        
        public void AddExperience(int range)
        {
            int xp = Math.Min(CurrentExperience + range, RequiredExperience);
            CurrentExperience = xp;
            OnExperienceChanged?.Invoke(xp, RequiredExperience);
        }
        
        public void ResetExperience()
        {
            CurrentExperience = 0;
            CurrentLevel = 1;
            OnExperienceChanged?.Invoke(CurrentExperience, RequiredExperience);
            OnLevelUp?.Invoke(CurrentLevel);
        }
        
        public void LevelUp()
        {
            if (CanLevelUp())
            {
                CurrentExperience = 0;
                CurrentLevel++;
                OnLevelUp?.Invoke(CurrentLevel);
                OnExperienceChanged?.Invoke(CurrentExperience, RequiredExperience);
            }
        }

        public bool CanLevelUp()
        {
            return CurrentExperience == RequiredExperience;
        }
    }
}