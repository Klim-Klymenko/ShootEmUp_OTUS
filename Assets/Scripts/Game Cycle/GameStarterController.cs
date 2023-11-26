using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(GameStarterTimer))]
    [RequireComponent(typeof(GameManager))]
    [RequireComponent(typeof(StartButtonManager))]
    [RequireComponent(typeof(SwitchStateComponent))]
    public sealed class GameStarterController : MonoBehaviour, IGameRunner,
        IGameFinishListener, IGameResumeListener, IGamePauseListener
    {
        [SerializeField] private GameStarterTimer _starterTimer;
        [SerializeField] private StartButtonManager _startButtonManager;
        private IGameStartable _gameManager;

        private bool HasGameRun
        {
            get => _gameManager.HasGameRun;
            set => _gameManager.HasGameRun = value;
        }

        private bool HasGameStarted => _gameManager.HasGameStarted;
        
        private GameState GameState => _gameManager.CurrentGameState;

        [SerializeField] private SwitchStateComponent _switchComponent;
        public bool IsOnlyUnityMethods { get; } = true;

        private void OnValidate()
        {
            _starterTimer = GetComponent<GameStarterTimer>();
            _switchComponent = GetComponent<SwitchStateComponent>();
            _startButtonManager = GetComponent<StartButtonManager>();
        }

        private void Awake() => _gameManager = GetComponent<GameManager>();
        
        public void Enable()
        {
            _starterTimer.OnGameStarted += StartGame;
            
            if (HasGameRun && GameState == GameState.Finished) 
                return;
            
            _startButtonManager.DisableStartButton();

            HasGameRun = true;
        }

        private void Disable() => _starterTimer.OnGameStarted -= StartGame;
        
        private void Update() => _starterTimer.TimerCountdown(HasGameRun);

        private void StartGame() => _gameManager.OnStart();

        void IGameFinishListener.OnFinish()
        {
            Disable();

            _switchComponent.TurnOff(this);
        }

        void IGameResumeListener.OnResume()
        {
            _switchComponent.TurnOn(this);
            
            if (!HasGameStarted)
                Enable();
        }
        
        void IGamePauseListener.OnPause()
        {
            Disable();

            _switchComponent.TurnOff(this);
        }
    }
}