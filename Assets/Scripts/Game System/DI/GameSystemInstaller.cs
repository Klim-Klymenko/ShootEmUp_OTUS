using UnityEngine;
using Zenject;
using SO;

namespace GameSystem
{
    internal sealed class GameSystemInstaller : MonoInstaller
    {
        [SerializeField] 
        private CharacterConfig _characterConfig;

        public override void InstallBindings()
        {
            BindModels();
        }

        private void BindModels()
        {
            Container.Bind<UserInfo>().FromMethod(() =>
            {
                UserInfo userInfo = new();

                userInfo.ChangeName(_characterConfig.Name);
                userInfo.ChangeDescription(_characterConfig.Description);
                userInfo.ChangeIcon(_characterConfig.Icon);

                return userInfo;
            }).AsSingle();
            
            Container.Bind<PlayerLevel>().AsSingle();
            
            Container.Bind<CharacterStats>().FromMethod(() =>
            {
                CharacterStats characterStats = new();

                for (int i = 0; i < _characterConfig.ValuesLength; i++)
                {
                    CharacterStat characterStat = new()
                    {
                        Name = _characterConfig.StatNames[i]
                    };

                    characterStat.ChangeValue(_characterConfig.Values[i]);
                    
                    characterStats.AddStat(characterStat);
                }
                
                return characterStats;
            }).AsSingle();
        }
    }
}