using System;
using UnityEngine;

namespace PM
{
    public sealed class UserInfoPresenter : IDisposable
    {
        private readonly UserInfo _userInfo;
        private PopupView _popupView;
        
        public UserInfoPresenter(UserInfo userInfo)
        {
            _userInfo = userInfo;
        }

        public void Construct(PopupView popupView)
        {
            _popupView = popupView;

            _userInfo.OnNameChanged += UpdateName;
            _userInfo.OnDescriptionChanged += UpdateDescription;
            _userInfo.OnIconChanged += UpdateAvatar;
        }
        
        public void Dispose()
        {
            _userInfo.OnNameChanged -= UpdateName;
            _userInfo.OnDescriptionChanged -= UpdateDescription;
            _userInfo.OnIconChanged -= UpdateAvatar;
        }

        private void UpdateName(string name)
        {
            _popupView.UpdateName(name);
        }

        private void UpdateDescription(string description)
        {
            _popupView.UpdateDescription(description);
        }

        private void UpdateAvatar(Sprite icon)
        {
            _popupView.UpdateAvatar(icon);
        } 
    }
}