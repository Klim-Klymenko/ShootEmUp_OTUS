using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp
{
    [System.Serializable]
    public sealed class ResumePauseButtonsController : IGameInitializeListener, IGameFinishListener
    {
        [SerializeField]
        private Button _resumeButton;
        
        [SerializeField] 
        private Button _pauseButton;

        private GameResumePauseDecorator _resumePauseDecorator;

        [Inject]
        public void Construct(GameResumePauseDecorator resumePauseDecorator)
        {
            _resumePauseDecorator = resumePauseDecorator;
        }

        void IGameInitializeListener.OnInitialize()
        {
            _resumeButton.onClick.AddListener(OnResume);
            _pauseButton.onClick.AddListener(OnPause);
        }

        void IGameFinishListener.OnFinish()
        {
            _resumeButton.onClick.RemoveListener(OnResume);
            _pauseButton.onClick.RemoveListener(OnPause);
        }
        
        private void OnResume() => _resumePauseDecorator.OnResume();
        private void OnPause() => _resumePauseDecorator.OnPause();
    }
}