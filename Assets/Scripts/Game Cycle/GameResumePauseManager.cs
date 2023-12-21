using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameResumePauseManager : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;

        [SerializeField] private GameStarterController _starterController;

        private bool HasGameStarted => _gameManager.HasGameStarted;
        
        public void OnResume()
        {
            if (!_gameManager.HasGameRun) return;
            
            if (!HasGameStarted)
                _starterController.OnResume();
            else
                _gameManager.OnResume();
        }
        
        public void OnPause()
        {
            if (!_gameManager.HasGameRun) return;
            
            if (!HasGameStarted)
                _starterController.OnPause();
            else
                _gameManager.OnPause();
        }
    }
}