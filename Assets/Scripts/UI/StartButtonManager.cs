using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp
{
    [System.Serializable]
    public sealed class StartButtonManager : IGameInitializeListener, IGameFinishListener
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private GameObject _startButtonGameObject;

        private GameStarterController _gameStarter;
        
        [Inject]
        private void Construct(GameStarterController gameStarter)
        {
            _gameStarter = gameStarter;
        }

        void IGameInitializeListener.OnInitialize()
        {
            _startButton.onClick.AddListener(DisableStartButton);
            _startButton.onClick.AddListener(StartGame);
        }

        void IGameFinishListener.OnFinish()
        {
            _startButton.onClick.RemoveListener(DisableStartButton);
            _startButton.onClick.RemoveListener(StartGame);
        }

        private void DisableStartButton() => _startButtonGameObject.SetActive(false);
        private void StartGame() => _gameStarter.Enable();
    }
}