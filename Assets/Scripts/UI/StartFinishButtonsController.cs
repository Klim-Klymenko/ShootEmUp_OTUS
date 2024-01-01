using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp
{
    [System.Serializable]
    public sealed class StartFinishButtonsController : IGameInitializeListener, IGameFinishListener
    {
        [SerializeField]
        private Button _finishButton;
        
        [SerializeField]
        private Button _startButton;
        
        [SerializeField]
        private GameObject _startButtonGameObject;

        private GameStarterController _gameStarter;
        private GameManager _gameManager;
        
        [Inject]
        public void Construct(GameStarterController gameStarter, GameManager gameManager)
        {
            _gameStarter = gameStarter;
            _gameManager = gameManager;
        }
        
        void IGameInitializeListener.OnInitialize()
        {
            _startButton.onClick.AddListener(DisableStartButton);
            _startButton.onClick.AddListener(StartGame);
            
            _finishButton.onClick.AddListener(OnFinish);
        }

        void IGameFinishListener.OnFinish()
        {
            _startButton.onClick.RemoveListener(DisableStartButton);
            _startButton.onClick.RemoveListener(StartGame);
            
            _finishButton.onClick.RemoveListener(OnFinish);
        }

        private void DisableStartButton() => _startButtonGameObject.SetActive(false);
        private void StartGame() => _gameStarter.Enable();
        
        private void OnFinish() => _gameManager.OnFinish();
    }
}