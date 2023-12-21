using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(GameStarterTimer))]
    [RequireComponent(typeof(GameManager))]
    public sealed class GameStarterController : MonoBehaviour, IGameFinishListener
    {
        [SerializeField] private GameStarterTimer _starterTimer;
        private IGameStartable _gameManager;

        private bool HasGameRun
        {
            get => _gameManager.HasGameRun;
            set => _gameManager.HasGameRun = value;
        }
        
        private void OnValidate() => _starterTimer = GetComponent<GameStarterTimer>();
        
        private void Awake() => _gameManager = GetComponent<GameManager>();
        
        public void Enable()
        {
            _starterTimer.OnGameStarted += StartGame;
            
            HasGameRun = true;
        }

        private void Disable() => _starterTimer.OnGameStarted -= StartGame;

        private void Update() => _starterTimer.TimerCountdown(HasGameRun);

        private void StartGame() => _gameManager.OnStart();

        void IGameFinishListener.OnFinish()
        {
            Disable();
            enabled = false;
        }

        public void OnResume()
        {
            enabled = true;
            Enable();
        }
        
        public void OnPause()
        {
            Disable();
            enabled = false;
        }
    }
}