using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace GameCycle
{
    [UsedImplicitly]
    internal sealed class GameCycleManagerInstaller
    {
        private readonly GameCycleManager _gameCycleManager;
        private readonly List<IGameListener> _gameListeners;
        
        internal GameCycleManagerInstaller(GameCycleManager gameCycleManager, List<IGameListener> gameListeners)
        {
            _gameCycleManager = gameCycleManager;
            _gameListeners = gameListeners;
        }

        internal void InstallListeners()
        {
            InstallSystemListeners();
            InstallSceneListeners();
        }
        
        private void InstallSystemListeners()
        {
            for (int i = 0; i < _gameListeners.Count; i++)
                _gameCycleManager.AddListener(_gameListeners[i]);
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
    }
}