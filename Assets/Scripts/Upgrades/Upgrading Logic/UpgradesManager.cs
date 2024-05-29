using System;
using System.Collections.Generic;
using System.Linq;
using GameEngine;
using Sirenix.OdinInspector;

namespace Upgrades.UpgradingLogic
{
    internal sealed class UpgradesManager
    {
        public event Action<Upgrade> OnLevelUp;
        
        [ReadOnly, ShowInInspector]
        private Dictionary<string, Upgrade> _upgrades = new();

        private readonly MoneyStorage _moneyStorage;

        internal UpgradesManager(MoneyStorage moneyStorage)
        {
            _moneyStorage = moneyStorage;
        }
        
        internal void Setup(Upgrade[] upgrades)
        {
            _upgrades = new Dictionary<string, Upgrade>();
            
            for (int i = 0, count = upgrades.Length; i < count; i++)
            {
                Upgrade upgrade = upgrades[i];
                _upgrades[upgrade.Id] = upgrade;
            }
        }

        public Upgrade GetUpgrade(string id)
        {
            return _upgrades[id];
        }

        public Upgrade[] GetAllUpgrades()
        {
            return _upgrades.Values.ToArray();
        }

        public bool CanLevelUp(Upgrade upgrade)
        {
            if (!upgrade.CanLevelUp())
                return false;

            int price = upgrade.NextPrice;
            return _moneyStorage.CanSpendMoney(price);
        }

        public void LevelUp(Upgrade upgrade)
        {
            if (!CanLevelUp(upgrade))
                throw new Exception($"Can not level up {upgrade.Id}");

            int price = upgrade.NextPrice;
            _moneyStorage.SpendMoney(price);

            upgrade.LevelUp();
            OnLevelUp?.Invoke(upgrade);
        }

        [Title("Methods")]
        [Button]
        public bool CanLevelUp(string id)
        {
            Upgrade upgrade = _upgrades[id];
            return CanLevelUp(upgrade);
        }

        [Button]
        public void LevelUp(string id)
        {
            Upgrade upgrade = _upgrades[id];
            LevelUp(upgrade);
        }
    }
}