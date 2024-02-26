using UnityEngine;
using Zenject;

namespace GameEngine
{
    public sealed class GameSystemInstaller : MonoInstaller
    {
        [SerializeField]
        private GameSystem _gameSystem;
        
        public override void InstallBindings()
        {
            InstallGameSystem();
        }

        private void InstallGameSystem()
        {
            Container.BindInterfacesTo<GameSystem>().FromInstance(_gameSystem).AsSingle();
        }
    }
}