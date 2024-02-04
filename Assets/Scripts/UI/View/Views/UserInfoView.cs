using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PM
{
    internal sealed class UserInfoView : MonoBehaviour
    {
        [SerializeField]
        private Text _nameText;
        
        [SerializeField]
        private Text _descriptionText;
        
        [SerializeField]
        private Image _avatar;

        private readonly CompositeDisposable _disposable = new();
        
        private IUserInfoPresenter _presenter;
        
        [Inject]
        internal void Construct(IUserInfoPresenter presenter)
        {
            _presenter = presenter;
        }
        
        internal void Show()
        {
            _presenter.Name.Subscribe(UpdateName).AddTo(_disposable);
            _presenter.Description.Subscribe(UpdateDescription).AddTo(_disposable);
            _presenter.Icon.Subscribe(UpdateAvatar).AddTo(_disposable);
        }
        
        internal void Hide()
        {
            _disposable.Clear();
        }

        private void UpdateName(string name)
        {
            _nameText.text = name;  
        }

        private void UpdateDescription(string description)
        {
            _descriptionText.text = description;
        }

        private void UpdateAvatar(Sprite icon)
        {
            _avatar.sprite = icon;
        }
    }
}