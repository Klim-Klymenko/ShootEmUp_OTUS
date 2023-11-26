using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(GameManager))]
    public sealed class GameManagerInstaller : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;
        
        private void OnValidate() => _gameManager = GetComponent<GameManager>();

        private void Awake()
        {
            MonoBehaviour[] allScripts = FindObjectsOfType<MonoBehaviour>(true);
            
            for (int i = 0; i < allScripts.Length; i++)
            {
                if (allScripts[i] is IGameListener gameListener)
                {
                    _gameManager.AddUpdateListeners(gameListener);
                    _gameManager.AddEventListeners(gameListener);

                    if (gameListener is IGameInitializeListener initializeListener)
                        initializeListener.OnInitialize();
                    
                    if (gameListener is not IGameRunner)
                        allScripts[i].enabled = false;
                }
            }

            _gameManager.CurrentGameState = GameState.Initialized;
        }
    }
}

