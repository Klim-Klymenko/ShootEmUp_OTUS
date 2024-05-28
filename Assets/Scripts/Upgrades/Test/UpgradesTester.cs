using System.Collections.Generic;
using Game.Gameplay.Player;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Sample
{
    internal sealed class UpgradesTester : MonoBehaviour
    {
        [Space]
        [ReadOnly]
        [SerializeField]
        [UsedImplicitly]
        private string[] _upgradeIds = {"Speed Upgrade", "Damage Upgrade", "Health Upgrade", "Armor Upgrade"};
        
        private UpgradesManager _upgradesManager;
        private MoneyStorage _moneyStorage;
        private PlayerStats _playerStats;

        [Inject]
        internal void Construct(UpgradesManager upgradesManager, MoneyStorage moneyStorage, PlayerStats playerStats)
        {
            _upgradesManager = upgradesManager;
            _moneyStorage = moneyStorage;
            _playerStats = playerStats;
        }

        [Button]
        private void ApplyUpgrade(string id)
        {
            _upgradesManager.LevelUp(id);
            OnUpgradeApplied();
        }

        private void OnUpgradeApplied()
        {
            Debug.Log($"Money balance: {_moneyStorage.Money}");
            
            IReadOnlyDictionary<string, int> stats = _playerStats.GetStats();
            
            foreach (KeyValuePair<string, int> stat in stats)
            {
                Debug.Log($"Stats' updated values: {stat.Key} = {stat.Value}");
            }
        }
    }
}