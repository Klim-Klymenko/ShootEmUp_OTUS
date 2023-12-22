namespace ShootEmUp
{
    public sealed class GameResumePauseDecorator
    {
        private GameManager _gameManager;
        private GameStarterController _starterController;

        private bool HasGameStarted => _gameManager.HasGameStarted;

        [Inject]
        private void Construct(GameManager gameManager, GameStarterController starterController)
        {
            _gameManager = gameManager;
            _starterController = starterController;
        }
        
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