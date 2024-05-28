using Game.Gameplay.Player;
using Sample;
using UnityEngine;
using Zenject;

namespace Game_Engine.DI
{
    internal sealed class GameEngineInstaller : MonoInstaller
    {
        [SerializeField]
        private int _money;
        
        public override void InstallBindings()
        {
            BindMoneyStorage();
            BindStats();
        }
        
        private void BindMoneyStorage()
        {
            Container.Bind<MoneyStorage>().FromMethod(() =>
            {
                MoneyStorage moneyStorage = new();
                moneyStorage.SetupMoney(_money);

                return moneyStorage;
            }).AsSingle();
        }

        private void BindStats()
        {
            Container.Bind<PlayerStats>().AsSingle();
        }
    }
}