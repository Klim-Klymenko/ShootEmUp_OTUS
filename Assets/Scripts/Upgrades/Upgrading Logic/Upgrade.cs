using System;
using Sirenix.OdinInspector;

namespace Sample
{
    public abstract class Upgrade
    {
        public event Action<int> OnLevelUp;

        [ShowInInspector, ReadOnly]
        public string Id => _config.Id;

        [ShowInInspector, ReadOnly]
        public int Level => _currentLevel;

        [ShowInInspector, ReadOnly]
        public int MaxLevel => _config.MaxLevel;

        public bool IsMaxLevel => _currentLevel == _config.MaxLevel;

        [ShowInInspector, ReadOnly]
        public float Progress => (float) _currentLevel / _config.MaxLevel;

        [ShowInInspector, ReadOnly]
        public int NextPrice => _config.GetPrice(Level + 1);
        
        private int _currentLevel;

        private readonly UpgradeConfig _config;
        
        private protected Upgrade(UpgradeConfig config)
        {
            _config = config;
            _currentLevel = 1;
        }

        internal void SetupLevel(int level)
        {
            _currentLevel = level;
        }

        public void LevelUp()
        {
            if (Level >= MaxLevel)
                throw new Exception($"Can not increment level for upgrade {_config.Id}!");
            
            _currentLevel++;
            LevelUp(_currentLevel);
            OnLevelUp?.Invoke(_currentLevel);
        }

        private protected abstract void LevelUp(int level);
    }
}