using System;
using JetBrains.Annotations;
using Sirenix.OdinInspector;

namespace Game.Gameplay.Player
{
    [UsedImplicitly]
    public sealed class MoneyStorage
    {
        public event Action<int> OnMoneyChanged;
        public event Action<int> OnMoneyEarned;
        public event Action<int> OnMoneySpent;
        
        [ReadOnly, ShowInInspector]
        public int Money => _money;

        private int _money;

        [Title("Methods")]
        [Button]
        [GUIColor(0, 1, 0)]
        public void EarnMoney(int amount)
        {
            if (amount == 0) return;
            
            if (amount < 0)
                throw new Exception($"Can not earn negative money {amount}");
            
            _money += amount;
            
            OnMoneyChanged?.Invoke(_money);
            OnMoneyEarned?.Invoke(amount);
        }

        [Button]
        [GUIColor(0, 1, 0)]
        public void SpendMoney(int amount)
        {
            if (amount == 0) return;
            
            if (amount < 0)
                throw new Exception($"Can not spend negative money {amount}");
            
            _money -= amount;
            
            if (_money < 0)
                throw new Exception($"Negative money after spend. Money in bank: {_money}, spend amount {amount} ");
            
            OnMoneyChanged?.Invoke(_money);
            OnMoneySpent?.Invoke(amount);
        }

        [Button]
        [GUIColor(0, 1, 0)]
        public void SetupMoney(int money)
        {
            _money = money;
            OnMoneyChanged?.Invoke(money);
        }

        public bool CanSpendMoney(int amount)
        {
            return _money >= amount;
        }
    }
}