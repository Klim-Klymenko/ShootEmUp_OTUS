using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(GameManager))]
    public sealed class GameManagerInstaller : MonoBehaviour
    {
        private GameManager _gameManager;

        private void Awake()
        {
            _gameManager = GetComponent<GameManager>();

            MonoBehaviour[] allScripts = FindObjectsOfType<MonoBehaviour>(true);
            
            for (int i = 0; i < allScripts.Length; i++)
            {
                if (allScripts[i] is IGameListener gameListener)
                {
                    _gameManager.AddUpdateListeners(gameListener);
                    _gameManager.AddEventListeners(gameListener);
                    
                    if (allScripts[i] is IGameStartListener startListener)
                        startListener.OnStart();
                }
            }
        }
    }
}

