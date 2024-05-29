using System;
using Common;
using Sirenix.OdinInspector;
using Upgrades.Configs;

namespace Upgrades.UpgradingLogic
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
        
        protected ICondition[] Conditions { private get; set; }
        
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

        internal void LevelUp()
        {
            if (!CanLevelUp())
                throw new Exception($"Can not increment level for upgrade {_config.Id}!");
            
            _currentLevel++;
            LevelUp(_currentLevel);
            OnLevelUp?.Invoke(_currentLevel);
        }

        internal bool CanLevelUp()
        {
            if (Level >= MaxLevel)
                return false;

            if (Conditions == null)
                return true;
            
            for (int i = 0; i < Conditions.Length; i++)
            {
                if (!Conditions[i].Invoke())
                    return false;
            }

            return true;
        }
        
        private protected abstract void LevelUp(int upgradedLevel);
    }
}