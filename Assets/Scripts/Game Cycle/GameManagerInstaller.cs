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
                    _gameManager.AddListeners(gameListener);
            }
        }
    }
}

