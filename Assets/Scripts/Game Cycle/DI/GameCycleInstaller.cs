using UnityEngine;
using Zenject;

namespace GameCycle
{
    internal sealed class GameCycleInstaller : MonoInstaller, IFinishGameListener
    {
        [SerializeField]
        private SceneContext _sceneContext;
        
        private GameCycleManagerInstaller _gameCycleManagerInstaller;
        
        public override void InstallBindings()
        {
            BindGameCycleManager();
            BindGameCycleManagerInstaller();
            
            _sceneContext.PostResolve += InstallListeners;
        }

        void IFinishGameListener.OnFinish()
        {
            _sceneContext.PostResolve -= InstallListeners;
        }

        private void BindGameCycleManager()
        {
            Container.Bind<GameCycleManager>().FromComponentInHierarchy().AsSingle();
        }

        private void BindGameCycleManagerInstaller()
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