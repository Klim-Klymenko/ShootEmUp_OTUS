using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    internal sealed class GameManagerInstaller
    {
        private readonly GameManager _gameManager;

        internal GameManagerInstaller(GameManager gameManager)
        {
            _gameManager = gameManager;
        }
        
        internal void InstallListeners(IEnumerable<Installer> installers)
        {
            foreach (var installer in installers)
                _gameManager.AddGameListeners(installer.ProvideGameListeners());
            
            MonoBehaviour[] sceneComponents = Object.FindObjectsOfType<MonoBehaviour>(true);

            for (int i = 0; i < sceneComponents.Length; i++)
            {
                if (sceneComponents[i] is IGameListener gameListener)
                    _gameManager.AddGameListener(gameListener);
            }
        }
    }
}