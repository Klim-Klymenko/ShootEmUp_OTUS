using UnityEngine;

namespace GameCycle
{
    internal sealed class GameCycleManagerInstaller
    {
        private readonly GameCycleManager _gameCycleManager;
        private readonly IGameListener[] _gameListeners;

        public GameCycleManagerInstaller(GameCycleManager gameCycleManager)
        {
            _gameCycleManager = gameCycleManager;
        }

        public void InstallListeners()
        {
            InstallSceneListeners();
            InstallPlainListeners();
        }
        
        private void InstallSceneListeners()
        {
            MonoBehaviour[] sceneComponents = Object.FindObjectsOfType<MonoBehaviour>(true);
            
            for (int i = 0; i < sceneComponents.Length; i++)
            {
                if (sceneComponents[i] is IGameListener gameListener)
                    _gameCycleManager.AddListener(gameListener);
            }
        }

        private void InstallPlainListeners()
        {
            for (int i = 0; i < _gameListeners.Length; i++)
                _gameCycleManager.AddListener(_gameListeners[i]);
        }
    }
}