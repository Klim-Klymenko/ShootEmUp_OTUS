using UnityEngine;
using Zenject;

namespace GameCycle
{
    internal sealed class GameCycleInstaller : MonoInstaller
    {
        [SerializeField]
        private SceneContext _sceneContext;
        
        private GameCycleManagerInstaller _gameCycleManagerInstaller;
        
        public override void InstallBindings()
        {
            InstallGameCycleManager();
            InstallGameCycleManagerInstaller();

            _sceneContext.PostResolve += InstallListeners;
        }

        private void OnDestroy()
        {
            _sceneContext.PostInstall -= InstallListeners;
        }

        private void InstallGameCycleManager()
        {
            Container.Bind<GameCycleManager>().FromComponentInHierarchy().AsSingle();
        }

        private void InstallGameCycleManagerInstaller()
        {
            Container.Bind<GameCycleManagerInstaller>().AsSingle()
                .OnInstantiated<GameCycleManagerInstaller>((_, it) => _gameCycleManagerInstaller = it).NonLazy();
        }
        
        private void InstallListeners()
        {
            _gameCycleManagerInstaller.InstallListeners();
        }
    }
}