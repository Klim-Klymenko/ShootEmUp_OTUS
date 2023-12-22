using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameStarterController : MonoBehaviour, IGameInitializeListener, IGameFinishListener
    {
        private bool HasGameRun
        {
            get => _gameManager.HasGameRun;
            set => _gameManager.HasGameRun = value;
        }
        
        private readonly GameStarterTimer _starterTimer = new();
        private GameManager _gameManager;
        
        [Inject]
        private void Construct(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        void IGameInitializeListener.OnInitialize() => _starterTimer.InitTimer();
        
        public void Enable()
        {
            _starterTimer.OnGameStarted += StartGame;
            
            HasGameRun = true;
            enabled = true;
        }

        private void StartGame() => _gameManager.OnStart();
        
        private void Disable()
        {
            _starterTimer.OnGameStarted -= StartGame;
            
            enabled = false;
        }

        void IGameFinishListener.OnFinish() => Disable();
        
        private void Update() => _starterTimer.TimerCountdown(HasGameRun);
        
        public void OnResume() => Enable();

        public void OnPause() => Disable();
    }
}