using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PM
{
    internal sealed class PlayerLevelView : MonoBehaviour
    {
        [SerializeField]
        private Text _experienceText;
     
        [SerializeField]
        private Text _levelText;
        
        [SerializeField]
        private Button _levelUpButton;

        private readonly CompositeDisposable _disposable = new();
        
        private IPlayerLevelPresenter _presenter;
        
        [Inject]
        internal void Construct(IPlayerLevelPresenter presenter)
        {
            _presenter = presenter;
        }

        internal void Show()
        {
            _presenter.Experience.Subscribe(UpdateExperience).AddTo(_disposable);
            _presenter.Level.Subscribe(UpdateLevel).AddTo(_disposable);
            
            _levelUpButton.onClick.AddListener(_presenter.LevelUp);
            _levelUpButton.onClick.AddListener(UpdateLevelUpButton);
        }

        internal void Hide()
        {
            _presenter.OnClosed?.Invoke();
            
            _disposable.Clear();

            _levelUpButton.onClick.RemoveListener(_presenter.LevelUp);
            _levelUpButton.onClick.RemoveListener(UpdateLevelUpButton);
        }
        
        private void UpdateLevelUpButton()
        {
            _levelUpButton.gameObject.SetActive(_presenter.CanLevelUp());
        }
        
        private void UpdateExperience(string experience)
        {
            _experienceText.text = experience;
            
            UpdateLevelUpButton();
        }

        private void UpdateLevel(string level)
        {
            _levelText.text = level;
        }
    }
}